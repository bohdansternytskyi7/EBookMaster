using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EBookMasterClassLibrary.Models
{
	public class Series : Entity
	{
		[Required]
		[MaxLength(100)]
		public string Name { get; set; }

		public ICollection<Book> Books { get; set; } = new List<Book>();
	}
}
