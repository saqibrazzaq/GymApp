import { PlanTypeSearchReq } from "../models/Plan";
import { axiosInstance } from "../provider";

export const PlanTypeApi = {
  search: async function (searchParams?: PlanTypeSearchReq) {
    const response = await axiosInstance.request({
      url: "/PlanTypes/search",
      method: "GET",
      params: searchParams,
    });

    return response.data;
  },
};
