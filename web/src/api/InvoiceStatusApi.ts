import { InvoiceStatusSearchReq } from "../dtos/Invoice";
import { axiosInstance } from "../provider";

export const InvoiceStatusApi = {
  search: async function (searchParams?: InvoiceStatusSearchReq) {
    const response = await axiosInstance.request({
      url: "/InvoiceStatuses/search",
      method: "GET",
      params: searchParams,
    });

    return response.data;
  },
};
