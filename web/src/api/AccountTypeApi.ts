import { AccountTypeSearchReq } from "../dtos/Account";
import { axiosInstance } from "../provider";

export const AccountTypeApi = {
  search: async function (searchParams?: AccountTypeSearchReq) {
    const response = await axiosInstance.request({
      url: "/AccountTypes/search",
      method: "GET",
      params: searchParams,
    });

    return response.data;
  },
};
