namespace EBookMasterWebApi.DTOs
{
	public class BookBorrowingDTO
	{
		public DateTime BorrowingDate { get; set; }
		public DateTime? ReturnDate { get; set; }
		public BookDTO Book { get; set; }
	}
}
