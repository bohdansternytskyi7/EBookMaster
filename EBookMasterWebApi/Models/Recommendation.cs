using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EBookMasterWebApi.Models
{
	public class Recommendation : Entity
	{
		[Required]
		public int UserId { get; set; }

		public ICollection<Book> RecommendedBooks { get; set; }

		[ForeignKey(nameof(UserId))]
		public User User { get; set; }

		[Required]
		public DateTime IssueDate { get; set; }

		[Required]
		public string Description { get; set; }
	}
}
