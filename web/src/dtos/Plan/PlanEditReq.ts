export default class PlanEditReq {
  name?: string = "";
  description?: string = "";
  planCategoryId?: string = "";
  planTypeId?: string = "";
  duration?: number = 0;
  timeUnitId?: string = "";
  setupFee?: number = 0;
  price?: number = 0;
}
