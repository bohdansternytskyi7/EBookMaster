using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EBookMasterWebApi.Models
{
	public class BookBorrowing : Entity
	{
		[Required]
		public DateTime BorrowingDate { get; set; }

		public DateTime? ReturnDate { get; set; }

		[Required]
		public int BookId { get; set; }

		[ForeignKey(nameof(BookId))]
		public Book Book { get; set; }

		[Required]
		public int UserId { get; set; }

		[ForeignKey(nameof(UserId))]
		public User User { get; set; }
	}
}
