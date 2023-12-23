import { AddressRes } from "../Address";
import { StateRes } from "../Country";
import { UserRes } from "../User";
import InvoiceStatusRes from "./InvoiceStatusRes";

export default class InvoiceRes {
  invoiceId?: string = "";
  accountId?: string = "";
  userId?: string = "";
  user?: UserRes;
  email?: string = "";
  shippingAddressId?: string = "";
  shippingAddress?: AddressRes;
  fullName?: string = "";
  phone?: string = "";
  address1?: string = "";
  address2?: string = "";
  city?: string = "";
  stateId?: string = "";
  state?: StateRes;
  issueDate?: string = "";
  statusId?: string = "";
  status?: InvoiceStatusRes;
  amountPayable?: number = 0;
  amountDue?: number = 0;
}
