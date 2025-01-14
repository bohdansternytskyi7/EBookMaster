namespace EBookMasterWebApi.DTOs
{
	public class RecommendationDTO
	{
		public int Id { get; set; }
		public DateTime IssueDate { get; set; }
		public string Description { get; set; }
		public ICollection<BookDTO> RecommendedBooks { get; set; }
	}
}
