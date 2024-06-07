using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EBookMaster.Models
{
	public class BookBorrowing : Entity
	{
		[Required]
		public DateTime BorrowingDate { get; set; }

		[Required]
		public DateTime ReturnDate { get; set; }

		[Required]
		public int BookId { get; set; }

		[ForeignKey(nameof(BookId))]
		public required Book Book { get; set; }

		[Required]
		public int UserId { get; set; }

		[ForeignKey(nameof(UserId))]
		public required User User { get; set; }

		public int? ReviewId { get; set; }

		[ForeignKey(nameof(ReviewId))]
		public Review? Review { get; set; }

	}
}
