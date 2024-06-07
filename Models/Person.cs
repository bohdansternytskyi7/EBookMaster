using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EBookMaster.Models
{
	public abstract class Person : Entity
	{
		[Required]
		[MaxLength(100)]
		public required string Name { get; set; }
		[Required]
		[MaxLength(100)]
		public required string Surname { get; set; }
	}
}
