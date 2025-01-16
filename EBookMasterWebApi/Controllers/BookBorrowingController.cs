using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EBookMasterWebApi.Context;
using EBookMasterWebApi.DTOs;
using EBookMasterWebApi.Enums;
using EBookMasterWebApi.Models;
using Microsoft.EntityFrameworkCore;
using EBookMasterWebApi.Services.Interfaces;

namespace EBookMasterWebApi.Controllers
{
	[Authorize]
	[ApiController]
	[Route("[controller]")]
	public class BookBorrowingController : BaseController
	{
		public BookBorrowingController(EBookMasterDbContext context, IConfiguration configuration, IMapper mapper, IAccountsService accountsService)
			: base(context, configuration, mapper, accountsService)
		{ }

		[AllowAnonymous]
		[HttpGet("subscriptions")]
		public async Task<IActionResult> GetSubscriptions()
		{
			return Ok(await _context.Subscriptions.ToListAsync());
		}

		[HttpGet("books")]
		public async Task<IActionResult> GetBooksAsync()
		{
			var booksDTO = _mapper.Map<List<BookDTO>>(await _context.Books
				.Include(x => x.PublishingHouse)
				.Include(x => x.Series)
				.Include(x => x.Authors)
				.Include(x => x.Categories)
				.Where(x => x.Status == BookStatus.Available)
				.ToListAsync());

			var userId = GetCurrentUserId();
			var borrowings = await _context.BookBorrowings.Where(x => x.UserId == userId).ToListAsync();

			booksDTO.ForEach(x =>
			{
				x.Borrowed = borrowings.Exists(y => !y.ReturnDate.HasValue && y.BookId == x.Id);
				x.NotAllowed = false;
			});

			return Ok(booksDTO);
		}

		[HttpGet("history")]
		public async Task<IActionResult> GetBorrowHistoryAsync()
		{
			var userId = GetCurrentUserId();
			return Ok(_mapper.Map<List<BookBorrowingDTO>>(await _context.BookBorrowings
				.Include(x => x.Book)
					.ThenInclude(b => b.Authors)
				.Include(x => x.Book)
					.ThenInclude(b => b.Categories)
				.Include(x => x.Book)
					.ThenInclude(b => b.Series)
				.Include(x => x.Book)
					.ThenInclude(b => b.PublishingHouse)
				.Where(x => x.UserId == userId)
				.OrderByDescending(x => x.BorrowingDate)
				.ToListAsync()));
		}

		[HttpPost("borrow")]
		public async Task<IActionResult> BorrowBookAsync([FromQuery] int id)
		{
			var book = await _context.Books.FirstOrDefaultAsync(x => x.Id == id);
			if (book == null)
				return NotFound();

			var user = await GetCurrentUserAsync();
			if (user.Subscription.Type != SubscriptionType.Premium && book.IsPremium)
				return BadRequest("No 'Premium' subscription.");

			if (await _context.BookBorrowings.Where(x => x.BookId == id && x.UserId == user.Id && x.ReturnDate == null).AnyAsync())
				return BadRequest("The book is already on loan.");

			_context.BookBorrowings.Add(new BookBorrowing()
			{
				BookId = book.Id,
				UserId = user.Id,
				BorrowingDate = DateTime.Now
			});
			await _context.SaveChangesAsync();
			return Ok();
		}

		[HttpPost("return")]
		public async Task<IActionResult> ReturnBookAsync([FromQuery] int id)
		{
			var book = await _context.Books.FirstOrDefaultAsync(x => x.Id == id);
			if (book == null)
				return NotFound();

			var userId = GetCurrentUserId();
			var bookBorrowing = await _context.BookBorrowings.Where(x => x.BookId == id && x.UserId == userId && x.ReturnDate == null).FirstOrDefaultAsync();
			if (bookBorrowing == null)
				return BadRequest("The book has not been borrowed.");

			bookBorrowing.ReturnDate = DateTime.Now;
			await _context.SaveChangesAsync();
			return Ok();
		}
	}
}
