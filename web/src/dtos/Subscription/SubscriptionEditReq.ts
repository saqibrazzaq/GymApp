import { Common } from "../../utility";
import { UserRes } from "../User";

export default class SubscriptionEditReq {
  planId?: string = "";
  userId?: string = "";
  activeFrom?: string = Common.dateToString(new Date());
  activeTo?: string = Common.dateToString(new Date());

  user?: UserRes;
}
