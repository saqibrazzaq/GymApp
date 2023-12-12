import { TimeUnitSearchReq } from "../dtos/Plan";
import { axiosInstance } from "../provider";

export const TimeUnitApi = {
  search: async function (searchParams?: TimeUnitSearchReq) {
    const response = await axiosInstance.request({
      url: "/TimeUnits/search",
      method: "GET",
      params: searchParams,
    });

    return response.data;
  },
};
