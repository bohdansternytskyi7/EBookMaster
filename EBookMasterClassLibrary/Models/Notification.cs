using System;
using EBookMasterClassLibrary.Enums;

namespace EBookMasterClassLibrary.Models
{
	public class Notification : Entity
	{
		public int UserId { get; set; }
		public string Title { get; set; }
		public DateTime SendDate { get; set; }
		public NotificationStatus Status { get; set; } = NotificationStatus.New;
	}
}
