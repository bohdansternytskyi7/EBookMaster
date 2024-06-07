using System.ComponentModel.DataAnnotations;

namespace EBookMaster.Models
{
	public class Series : Entity
	{
		[Required]
		[MaxLength(100)]
		public required string Name { get; set; }

		public ICollection<Book> Books { get; set; } = new List<Book>();
	}
}
