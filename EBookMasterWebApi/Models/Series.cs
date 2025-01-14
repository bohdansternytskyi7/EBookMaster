using System.ComponentModel.DataAnnotations;

namespace EBookMasterWebApi.Models
{
	public class Series : Entity
	{
		[Required]
		[MaxLength(100)]
		public string Name { get; set; }

		[Required]
		public bool IsOver { get; set; }

		[Required]
		public DateTime FirstBookPublicationDate { get; set; }

		public ICollection<Book> Books { get; set; } = new List<Book>();
	}
}
