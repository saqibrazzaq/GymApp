import { DiscountTypeSearchReq } from "../dtos/Invoice";
import { axiosInstance } from "../provider";

export const DiscountTypeApi = {
  search: async function (searchParams?: DiscountTypeSearchReq) {
    const response = await axiosInstance.request({
      url: "/DiscountTypes/search",
      method: "GET",
      params: searchParams,
    });

    return response.data;
  },
};
