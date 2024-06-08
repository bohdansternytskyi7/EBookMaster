using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EBookMasterClassLibrary.Models
{
	public class Author : Person
	{
		[Required]
		public DateTime DateOfBirth { get; set; }

		[Required]
		[MaxLength(100)]
		public string Nationality { get; set; }

		public ICollection<Book> Books { get; set; } = new List<Book>();
	}
}
