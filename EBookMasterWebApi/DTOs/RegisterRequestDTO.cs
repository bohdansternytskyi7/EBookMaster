namespace EBookMasterWebApi.DTOs
{
	public class RegisterRequestDTO
	{
		public string Name { get; set; }
		public string Surname { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public int SubscriptionId { get; set; }
	}
}
