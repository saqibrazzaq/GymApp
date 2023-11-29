import { PaymentMethodSearchReq } from "../dtos/Payment";
import { axiosInstance } from "../provider";

export const PaymentMethodApi = {
  search: async function (searchParams?: PaymentMethodSearchReq) {
    const response = await axiosInstance.request({
      url: "/PaymentMethods/search",
      method: "GET",
      params: searchParams,
    });

    return response.data;
  },
};
