using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EBookMasterClassLibrary.Models
{
	public class Category : Entity
	{
		[Required]
		[MaxLength(100)]
		public string Name { get; set; }

		[Required]
		[MaxLength(100)]
		public string Description { get; set; }

		public ICollection<Book> Books { get; set; } = new List<Book>();
	}
}
