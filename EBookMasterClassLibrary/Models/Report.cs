using System;

namespace EBookMasterClassLibrary.Models
{
	public class Report : Entity
	{
		public DateTime Date { get; set; }
		public Book Book { get; set; }
		public int BorrowCount { get; set; }
		public int AverageRate { get; set; }
	}
}
