using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EBookMasterWebApi.Models
{
	public class Review : Entity
	{
		[Required]
		public int Rate { get; set; }

		[Required]
		[MaxLength(100)]
		public string Description { get; set; }

		[Required]
		public int BookBorrowingId { get; set; }

		[ForeignKey(nameof(BookBorrowingId))]
		public BookBorrowing BookBorrowing { get; set; }
	}
}
