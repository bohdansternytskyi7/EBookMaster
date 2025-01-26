using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EBookMasterWebApi.Models
{
	public class UserSubscription : Entity
	{
		[Required]
		public int SubscriptionId { get; set; }

		[ForeignKey(nameof(SubscriptionId))]
		public Subscription Subscription { get; set; }

		[Required]
		public DateTime BeginDate { get; set; }

		[Required]
		public DateTime EndDate { get; set; }
	}
}
