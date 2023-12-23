import { Common } from "../../utility";

export default class InvoiceEditReq {
  userId?: string = "";
  email?: string = "";
  shippingAddressId?: string = "";
  fullName?: string = "";
  phone?: string = "";
  address1?: string = "";
  address2?: string = "";
  city?: string = "";
  stateId?: string = "";
  issueDate?: string = Common.dateToString(new Date());
  statusId?: string = "";
}
