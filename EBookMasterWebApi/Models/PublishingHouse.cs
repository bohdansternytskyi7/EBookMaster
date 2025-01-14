using System.ComponentModel.DataAnnotations;

namespace EBookMasterWebApi.Models
{
	public class PublishingHouse : Entity
	{
		[Required]
		[MaxLength(100)]
		public string Name { get; set; }

		[Required]
		[MaxLength(100)]
		public string Country { get; set; }

		[Required]
		public DateTime FoundationDate { get; set; }

		public ICollection<Book> Books { get; set; } = new List<Book>();
	}
}
