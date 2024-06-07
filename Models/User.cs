using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EBookMaster.Enums;

namespace EBookMaster.Models
{
    public class User : Person
	{
		[Required]
		[MaxLength(100)]
		public required string Email { get; set; }

		[Required]
		[MaxLength(100)]
		public required string LibraryCardNumber { get; set; }

		[Required]
		public Role Role { get; set; }

		[Required]
		public int SubscriptionId { get; set; }

		[ForeignKey(nameof(SubscriptionId))]
		public required Subscription Subscription { get; set; }

		public ICollection<BookBorrowing> BookBorrowings { get; set; } = new List<BookBorrowing>();
	}
}
