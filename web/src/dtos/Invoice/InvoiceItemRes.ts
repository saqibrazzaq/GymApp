import InvoiceRes from "./InvoiceRes";

export default class InvoiceItemRes {
  invoiceItemId?: string = "";
  invoiceId?: string = "";
  invoice?: InvoiceRes;
  productId?: string = "";
  productName?: string = "";
  qty?: number = 0;
  price?: number = 0;
  totalPrice?: number = 0;
}
