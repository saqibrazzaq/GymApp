import { GenderSearchReq } from "../dtos/User";
import { axiosInstance } from "../provider";

export const GenderApi = {
  search: async function (searchParams?: GenderSearchReq) {
    const response = await axiosInstance.request({
      url: "/Genders/search",
      method: "GET",
      params: searchParams,
    });

    return response.data;
  },
};
