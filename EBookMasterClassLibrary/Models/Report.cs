using System;
using System.ComponentModel.DataAnnotations;

namespace EBookMasterClassLibrary.Models
{
	public class Report : Entity
	{
		[Required]
		public DateTime Date { get; set; }

		[Required]
		public Book Book { get; set; }

		[Required]
		public int BorrowCount { get; set; }

		[Required]
		public decimal AverageRate { get; set; }
	}
}
