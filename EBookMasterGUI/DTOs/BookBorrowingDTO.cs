using System;

namespace EBookMasterGUI.DTOs
{
	public class BookBorrowingDTO
	{
		public DateTime BorrowingDate { get; set; }

		public DateTime? ReturnDate { get; set; }
	}
}
