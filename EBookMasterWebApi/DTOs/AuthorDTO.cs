namespace EBookMasterWebApi.DTOs
{
	public class AuthorDTO
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }
		public DateTime DateOfBirth { get; set; }
		public string Nationality { get; set; }
		public ICollection<BookDTO> Books { get; set; }
	}
}
