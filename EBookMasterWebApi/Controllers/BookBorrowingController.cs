using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using EBookMasterWebApi.Context;
using EBookMasterWebApi.DTOs;
using EBookMasterWebApi.Enums;
using EBookMasterWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EBookMaster.Controllers
{
	[Authorize]
	[ApiController]
	[Route("[controller]")]
	public class BookBorrowingController : Controller
	{
		private readonly EBookMasterDbContext _context;
		private readonly IMapper _mapper;
		public BookBorrowingController(EBookMasterDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		[AllowAnonymous]
		[HttpGet("subscriptions")]
		public async Task<IActionResult> GetSubscriptions()
		{
			return Ok(await _context.Subscriptions.ToListAsync());
		}

		[HttpGet("books")]
		public async Task<IActionResult> GetBooksAsync()
		{
			return Ok(await _context.Books
				.Include(x => x.PublishingHouse)
				.Include(x => x.Series)
				.Include(x => x.Authors)
				.Include(x => x.Categories)
				.Include(x => x.BookBorrowings)
				.ToListAsync());
		}


		[HttpGet("borrowhistory")]
		public async Task<IActionResult> GetBorrowHistoryAsync([FromQuery] BorrowRequestDTO borrowRequestDto)
		{
			var user = await FindUserAsync();
			var book = await FindBookAsync(borrowRequestDto);

			var borrowHistory = await _context.BookBorrowings
				.Include(x => x.Book)
				.Where(x => x.BookId == book.Id && x.UserId == user.Id)
				.ToListAsync();

			return Ok(borrowHistory);
		}

		[HttpPost("borrow")]
		public async Task<IActionResult> BorrowBookAsync([FromQuery] BorrowRequestDTO borrowRequestDTO)
		{
			var book = await FindBookAsync(borrowRequestDTO);
			if (book == null)
				return NotFound();

			var user = await FindUserAsync();
			if (user!.Subscription.Type != SubscriptionType.Premium)
				return BadRequest("No 'Premium' subscription.");

			if (_context.BookBorrowings.Where(x => x.BookId == book.Id && x.UserId == user.Id && x.ReturnDate == null).Any())
				return BadRequest("The book is already on loan.");

			_context.BookBorrowings.Add(new BookBorrowing() {
				BookId = book.Id,
				UserId = user.Id,
				BorrowingDate = DateTime.Now
			});
			await _context.SaveChangesAsync();

			return Ok(book);
		}

		[HttpPost("return")]
		public async Task<IActionResult> ReturnBookAsync([FromQuery] BorrowRequestDTO borrowRequestDTO)
		{
			var book = await FindBookAsync(borrowRequestDTO);
			if (book == null)
				return NotFound();

			var user = await FindUserAsync();
			var bookBorrowing = await _context.BookBorrowings.Where(x => x.BookId == book.Id && x.UserId == user!.Id && x.ReturnDate == null).FirstOrDefaultAsync();
			if (bookBorrowing == null)
				return BadRequest("The book has not been borrowed.");

			bookBorrowing.ReturnDate = DateTime.Now;
			await _context.SaveChangesAsync();

			return Ok(book);
		}

		[HttpGet("info")]
		public async Task<IActionResult> GetBookInfoAsync([FromQuery] BorrowRequestDTO borrowRequestDTO)
		{
			var book = await FindBookAsync(borrowRequestDTO);
			if (book == null)
				return NotFound();

			var user = await FindUserAsync();

			return Ok(await _context.BookBorrowings
				.Where(x => x.UserId == user.Id && x.BookId == book.Id)
				.ToListAsync());
		}

		private async Task<Book?> FindBookAsync(BorrowRequestDTO borrowRequestDTO)
		{
			var books = await _context.Books
				.Include(x => x.Authors)
				.ToListAsync();

			return books
				.Where(x => x.Title == borrowRequestDTO.Title && string.Join(", ", x.Authors.Select(y => y.Name)) == borrowRequestDTO.Authors)
				.FirstOrDefault();
		}

		private async Task<User?> FindUserAsync()
		{
			var userEmail = User.FindFirstValue(ClaimTypes.NameIdentifier);
			return await _context.Users
				.Include(x => x.Subscription)
				.Where(x => x.Email == userEmail)
				.FirstOrDefaultAsync();
		}
	}
}
