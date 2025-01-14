using System.ComponentModel.DataAnnotations;
using EBookMasterWebApi.Enums;

namespace EBookMasterWebApi.Models
{
	public class Subscription : Entity
	{
		[Required]
		public SubscriptionType Type { get; set; }

		[Required]
		public SubscriptionPeriod Period { get; set; }

		[Required]
		public decimal Price { get; set; }
	}
}
