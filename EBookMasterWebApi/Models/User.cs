using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EBookMasterWebApi.Enums;

namespace EBookMasterWebApi.Models
{
    public class User : Person
	{
		[Required]
		[MaxLength(100)]
		public string Email { get; set; }

		[Required]
		[MaxLength(256)]
		public string Password { get; set; }

		[Required]
		[MaxLength(256)]
		public string? Salt { get; set; }

		[MaxLength(256)]
		public string? RefreshToken { get; set; }

		public DateTime? RefreshTokenExpiration { get; set; }

		[Required]
		public int LibraryCardNumber { get; set; }

		[Required]
		public Role Role { get; set; }

		[Required]
		public int UserSubscriptionId { get; set; }

		[ForeignKey(nameof(UserSubscriptionId))]
		public UserSubscription UserSubscription { get; set; }

		public ICollection<BookBorrowing> BookBorrowings { get; set; } = new List<BookBorrowing>();
	}
}
