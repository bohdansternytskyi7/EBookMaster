using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EBookMaster.Models
{
	public class Review : Entity
	{
		[Required]
		public int Rate { get; set; }

		[Required]
		[MaxLength(100)]
		public required string Description { get; set; }

		[Required]
		public int BookBorrowingId { get; set; }

		[ForeignKey(nameof(BookBorrowingId))]
		public required BookBorrowing BookBorrowing { get; set; }
	}
}
