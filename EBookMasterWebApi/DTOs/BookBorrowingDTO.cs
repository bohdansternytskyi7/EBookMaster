using EBookMasterWebApi.Models;

namespace EBookMasterWebApi.DTOs
{
	public class BookBorrowingDTO
	{
		public DateTime BorrowingDate { get; set; }
		public DateTime? ReturnDate { get; set; }
		public BookDTO Book { get; set; }
		public int BookId { get; set; }
		public User User { get; set; }
		public int UserId { get; set; }
	}
}
