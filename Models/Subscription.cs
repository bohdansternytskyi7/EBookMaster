using EBookMaster.Enums;
using System.ComponentModel.DataAnnotations;

namespace EBookMaster.Models
{
	public class Subscription : Entity
	{
		[Required]
		public SubscriptionType Type { get; set; }

		[Required]
		public SubscriptionPeriod Period { get; set; }

		[Required]
		public double Price { get; set; }
	}
}
