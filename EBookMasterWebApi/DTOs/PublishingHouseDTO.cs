namespace EBookMasterWebApi.DTOs
{
	public class PublishingHouseDTO
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Country { get; set; }
		public DateTime FoundationDate { get; set; }
		public ICollection<BookDTO> Books { get; set; }
	}
}
