using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EBookMasterWebApi.Context;
using EBookMasterWebApi.Controllers;
using EBookMasterWebApi.DTOs;
using Microsoft.EntityFrameworkCore;
using EBookMasterWebApi.Services.Interfaces;

namespace EBookMaster.Controllers
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
			return Ok(await _context.BookBorrowings
				.Include(x => x.Book)
					.ThenInclude(b => b.Authors)
				.Include(x => x.Book)
					.ThenInclude(b => b.Categories)
				.Include(x => x.Book)
					.ThenInclude(b => b.Series)
				.Include(x => x.Book)
					.ThenInclude(b => b.PublishingHouse)
				.Where(x => x.UserId == userId)
				.ToListAsync());
		}

		//[HttpPost("borrow")]
		//public async Task<IActionResult> BorrowBookAsync([FromQuery] BorrowRequestDTO borrowRequestDTO)
		//{
		//	var book = await FindBookAsync(borrowRequestDTO);
		//	if (book == null)
		//		return NotFound();

		//	var user = await FindUserAsync();
		//	if (user!.Subscription.Type != SubscriptionType.Premium)
		//		return BadRequest("No 'Premium' subscription.");

		//	if (_context.BookBorrowings.Where(x => x.BookId == book.Id && x.UserId == user.Id && x.ReturnDate == null).Any())
		//		return BadRequest("The book is already on loan.");

		//	_context.BookBorrowings.Add(new BookBorrowing() {
		//		BookId = book.Id,
		//		UserId = user.Id,
		//		BorrowingDate = DateTime.Now
		//	});
		//	await _context.SaveChangesAsync();

		//	return Ok(book);
		//}

		//[HttpPost("return")]
		//public async Task<IActionResult> ReturnBookAsync([FromQuery] BorrowRequestDTO borrowRequestDTO)
		//{
		//	var book = await FindBookAsync(borrowRequestDTO);
		//	if (book == null)
		//		return NotFound();

		//	var user = await FindUserAsync();
		//	var bookBorrowing = await _context.BookBorrowings.Where(x => x.BookId == book.Id && x.UserId == user!.Id && x.ReturnDate == null).FirstOrDefaultAsync();
		//	if (bookBorrowing == null)
		//		return BadRequest("The book has not been borrowed.");

		//	bookBorrowing.ReturnDate = DateTime.Now;
		//	await _context.SaveChangesAsync();

		//	return Ok(book);
		//}

		//[HttpGet("info")]
		//public async Task<IActionResult> GetBookInfoAsync([FromQuery] BorrowRequestDTO borrowRequestDTO)
		//{
		//	var book = await FindBookAsync(borrowRequestDTO);
		//	if (book == null)
		//		return NotFound();

		//	var user = await FindUserAsync();

		//	return Ok(await _context.BookBorrowings
		//		.Where(x => x.UserId == user.Id && x.BookId == book.Id)
		//		.ToListAsync());
		//}

		//private async Task<Book?> FindBookAsync(BorrowRequestDTO borrowRequestDTO)
		//{
		//	var books = await _context.Books
		//		.Include(x => x.Authors)
		//		.ToListAsync();

		//	return books
		//		.Where(x => x.Title == borrowRequestDTO.Title && string.Join(", ", x.Authors.Select(y => y.Name)) == borrowRequestDTO.Authors)
		//		.FirstOrDefault();
		//}
	}
}
