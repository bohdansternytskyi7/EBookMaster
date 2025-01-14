using System.ComponentModel.DataAnnotations;

namespace EBookMasterWebApi.Models
{
	public class Category : Entity
	{
		[Required]
		[MaxLength(100)]
		public string Name { get; set; }

		[Required]
		[MaxLength(200)]
		public string Description { get; set; }

		public ICollection<Book> Books { get; set; } = new List<Book>();
	}
}
