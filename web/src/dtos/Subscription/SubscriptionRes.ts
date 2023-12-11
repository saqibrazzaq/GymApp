import { Common } from "../../utility";
import { PlanRes } from "../Plan";
import { UserRes } from "../User";

export default class SubscriptionRes {
  subscriptionId?: string = "";
  planId?: string = "";
  plan?: PlanRes;
  userId?: string = "";
  user?: UserRes;
  activeFrom?: string = "";
  activeTo?: string = "";
  status?: boolean = false;
}
