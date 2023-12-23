import { Common } from "../../utility";
import { PagedReq } from "../Request";

export default class InvoiceItemSearchReq extends PagedReq {
  invoiceId?: string;
  // userId?: string;
  constructor(
    {
      pageNumber = 1,
      pageSize = Common.DEFAULT_PAGE_SIZE,
      orderBy = "",
      searchText = "",
    }: PagedReq,
    { invoiceId = "", userId = "" }
  ) {
    super({
      pageNumber: pageNumber,
      pageSize: pageSize,
      orderBy: orderBy,
      searchText: searchText,
    });
    this.invoiceId = invoiceId;
    // this.userId = userId;
  }
}
