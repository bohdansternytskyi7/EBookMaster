namespace EBookMasterWebApi.DTOs
{
	public class BookDTO
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public DateTime PublicationYear { get; set; }
		public PublishingHouseDTO PublishingHouse { get; set; }
		public SeriesDTO Series { get; set; }
		public ICollection<AuthorDTO> Authors { get; set; }
		public ICollection<CategoryDTO> Categories { get; set; }
		public ICollection<RecommendationDTO> Recommendations { get; set; }
		public bool Borrowed { get; set; }
		public bool NotAllowed { get; set; }
	}
}
