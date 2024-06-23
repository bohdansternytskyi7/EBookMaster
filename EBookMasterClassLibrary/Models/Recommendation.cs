using System.Collections.Generic;

namespace EBookMasterClassLibrary.Models
{
	public class Recommendation : Entity
	{
		public int UserId { get; set; }
		public ICollection<Book> RecommendedBooks { get; set; }
	}
}
