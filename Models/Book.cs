﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EBookMaster.Models
{
	public class Book : Entity
	{
		[Required]
		[MaxLength(100)]
		public string Title { get; set; }

		[Required]
		public int PublishingHouseId { get; set; }


		[ForeignKey(nameof(PublishingHouseId))]
		public PublishingHouse PublishingHouse { get; set; }

		[Required]
		public DateTime PublicationYear { get; set; }

		public int? SeriesId { get; set; }

		[ForeignKey(nameof(SeriesId))]
		public Series? Series { get; set; }

		public ICollection<Author> Authors { get; set; } = new List<Author>();

		public ICollection<Category> Categories { get; set; } = new List<Category>();
	}
}