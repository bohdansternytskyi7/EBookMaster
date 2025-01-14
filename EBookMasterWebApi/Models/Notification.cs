using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EBookMasterWebApi.Enums;

namespace EBookMasterWebApi.Models
{
	public class Notification : Entity
	{
		[Required]
		public int UserId { get; set; }

		[Required]
		[MaxLength(200)]
		public string Title { get; set; }

		public DateTime? SendDate { get; set; }

		[Required]
		public NotificationStatus Status { get; set; } = NotificationStatus.New;

		[ForeignKey(nameof(UserId))]
		public User User { get; set; }
	}
}
