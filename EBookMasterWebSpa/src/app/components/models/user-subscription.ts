import { SubscriptionPeriod } from "src/app/enums/subscription-period";
import { SubscriptionType } from "src/app/enums/subscription-type";

export interface UserSubscription {
  id: number,
  type: SubscriptionType,
  period: SubscriptionPeriod,
  price: number
}
