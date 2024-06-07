using System.ComponentModel.DataAnnotations;

namespace EBookMaster.Models
{
	public class PublishingHouse : Entity
	{
		[Required]
		[MaxLength(100)]
		public required string Name { get; set; }

		[Required]
		[MaxLength(100)]
		public required string Country { get; set; }

		[Required]
		public DateTime FoundationDate { get; set; }

		public ICollection<Book> Books { get; set; } = new List<Book>();
	}
}
