using System.ComponentModel.DataAnnotations;

namespace EBookMaster.Models
{
	public abstract class Person : Entity
	{
		[Required]
		[MaxLength(100)]
		public string Name { get; set; }
		[Required]
		[MaxLength(100)]
		public string Surname { get; set; }
	}
}
