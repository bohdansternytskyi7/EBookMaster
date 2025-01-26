using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EBookMasterWebApi.Attributes;
using EBookMasterWebApi.Enums;

namespace EBookMasterWebApi.Models
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

		[Required]
		[AllowedBookStatus(BookStatus.Available, BookStatus.UnAvailable)]
		public BookStatus Status { get; set; }

		[Required]
		public bool IsPremium { get; set; }

		public ICollection<Author> Authors { get; set; } = new List<Author>();

		public ICollection<Category> Categories { get; set; } = new List<Category>();

		public ICollection<BookBorrowing> BookBorrowings { get; set; } = new List<BookBorrowing>();
		public ICollection<Recommendation> Recommendations { get; set; } = new List<Recommendation>();
	}
}
