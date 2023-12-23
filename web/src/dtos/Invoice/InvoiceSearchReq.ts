import { Common } from "../../utility";
import { PagedReq } from "../Request";

export default class InvoiceSearchReq extends PagedReq {
  // planId?: string;
  // userId?: string;
  constructor(
    {
      pageNumber = 1,
      pageSize = Common.DEFAULT_PAGE_SIZE,
      orderBy = "",
      searchText = "",
    }: PagedReq,
    { planId = "", userId = "" }
  ) {
    super({
      pageNumber: pageNumber,
      pageSize: pageSize,
      orderBy: orderBy,
      searchText: searchText,
    });
    // this.planId = planId;
    // this.userId = userId;
  }
}
