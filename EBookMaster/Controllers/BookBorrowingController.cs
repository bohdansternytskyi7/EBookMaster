using AutoMapper;
using EBookMaster.Models;
using EBookMasterClassLibrary.DTOs;
using EBookMasterClassLibrary.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using EBookMasterClassLibrary.Models;

namespace EBookMaster.Controllers
{
	[Authorize]
	[ApiController]
	[Route("api/[controller]")]
	public class BookBorrowingController : Controller
	{
		private readonly MainDbContext _context;
		private readonly IMapper _mapper;
		public BookBorrowingController(MainDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		[HttpGet("books")]
		public async Task<IActionResult> GetBooksAsync()
		{
			var bookBorrowings = await _context.Books
				.Include(x => x.PublishingHouse)
				.Include(x => x.Series)
				.Include(x => x.Authors)
				.Include(x => x.Categories)
				.ToListAsync();
			return Ok(bookBorrowings);
		}

		[HttpPost("borrow")]
		public async Task<IActionResult> BorrowBookAsync([FromQuery] BorrowRequestDTO borrowRequestDTO)
		{
			var books = await _context.Books
				.Include(x => x.Authors)
				.ToListAsync();

			var book = books
				.Where(x => x.Title == borrowRequestDTO.Title && string.Join(", ", x.Authors.Select(y => y.Name)) == borrowRequestDTO.Authors)
				.FirstOrDefault();

			if (book == null)
			{
				return NotFound();
			}

			var userEmail = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var user = await _context.Users
				.Include(x => x.Subscription)
				.Where(x => x.Email == userEmail)
				.FirstOrDefaultAsync();

			if (user!.Subscription.Type != SubscriptionType.Premium)
			{
				throw new ValidationException("Brak subskrypcji \"Premium\".");
			}

			_context.BookBorrowings.Add(new BookBorrowing() {
				BookId = book.Id,
				UserId = user.Id,
				BorrowingDate = DateTime.Now
			});
			await _context.SaveChangesAsync();

			return Ok(book);
		}
	}
}
