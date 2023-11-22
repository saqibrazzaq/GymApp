import { UserTypeSearchReq } from "../dtos/User";
import { axiosInstance } from "../provider";

export const UserTypeApi = {
  search: async function (searchParams?: UserTypeSearchReq) {
    const response = await axiosInstance.request({
      url: "/UserTypes/search",
      method: "GET",
      params: searchParams,
    });

    return response.data;
  },
};
