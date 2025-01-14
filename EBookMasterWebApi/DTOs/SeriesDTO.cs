namespace EBookMasterWebApi.DTOs
{
	public class SeriesDTO
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public bool IsOver { get; set; }
		public DateTime FirstBookPublicationDate { get; set; }
		public ICollection<BookDTO> Books { get; set; }
	}
}
