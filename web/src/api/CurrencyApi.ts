import { CurrencyEditReq, CurrencySearchReq } from "../dtos/Currency";
import { axiosInstance } from "../provider";

export const CurrencyApi = {
  create: async function (data?: CurrencyEditReq) {
    const response = await axiosInstance.request({
      url: "/Currencies",
      method: "POST",
      data: data,
    });

    return response.data;
  },
  update: async function (currencyId?: string, data?: CurrencyEditReq) {
    const response = await axiosInstance.request({
      url: "/Currencies/" + currencyId,
      method: "PUT",
      data: data,
    });

    return response.data;
  },
  delete: async function (currencyId?: string) {
    const response = await axiosInstance.request({
      url: `/Currencies/` + currencyId,
      method: "DELETE",
    });

    return response.data;
  },
  get: async function (currencyId?: string) {
    const response = await axiosInstance.request({
      url: `/Currencies/` + currencyId,
      method: "GET",
    });

    return response.data;
  },
  search: async function (searchParams?: CurrencySearchReq) {
    const response = await axiosInstance.request({
      url: "/Currencies/search",
      method: "GET",
      params: searchParams,
    });

    return response.data;
  },
};
