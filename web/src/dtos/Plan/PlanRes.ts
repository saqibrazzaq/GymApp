import PlanCategoryRes from "./PlanCategoryRes";
import PlanTypeRes from "./PlanTypeRes";
import TimeUnitRes from "./TimeUnitRes";

export default class PlanRes {
  planId?: string = "";
  accountId?: string = "";
  name?: string = "";
  description?: string = "";
  planCategoryId?: string = "";
  planCategory?: PlanCategoryRes;
  planTypeId?: string = "";
  planType?: PlanTypeRes;
  duration?: number;
  timeUnitId?: string = "";
  timeUnit?: TimeUnitRes;
  setupFee?: number;
  price?: number;
}
