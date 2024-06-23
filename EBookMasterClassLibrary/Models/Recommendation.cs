using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EBookMasterClassLibrary.Models
{
	public class Recommendation : Entity
	{
		[Required]
		public int UserId { get; set; }

		public ICollection<Book> RecommendedBooks { get; set; }

		[ForeignKey(nameof(UserId))]
		public User User { get; set; }
	}
}
