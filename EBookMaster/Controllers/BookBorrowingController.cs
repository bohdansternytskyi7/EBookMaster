using AutoMapper;
using EBookMaster.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
	}
}
