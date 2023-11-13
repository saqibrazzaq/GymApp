import PlanCategoryRes from "./PlanCategoryRes";
import PlanTypeRes from "./PlanTypeRes";

export default class PlanRes {
  planId?: string = "";
  accountId?: string = "";
  name?: string = "";
  description?: string = "";
  planCategoryId?: string = "";
  planCategory?: PlanCategoryRes;
  planTypeId?: string = "";
  planType?: PlanTypeRes;
}
