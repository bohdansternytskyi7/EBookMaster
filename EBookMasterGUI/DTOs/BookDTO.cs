using System.Collections.Generic;

namespace EBookMasterGUI.DTOs
{
	public class BookDTO
	{
		public string Title { get; set; }
		public string Authors { get; set; }
		public string PublishingHouse { get; set; }
		public int PublicationYear { get; set; }
		public string Series { get; set; }
		public string Categories { get; set; }
		public List<BookBorrowingDTO> BookBorrowings { get; set; }
	}
}
