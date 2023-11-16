import { LeadStatusSearchReq } from "../models/User";
import { axiosInstance } from "../provider";

export const LeadStatusApi = {
  search: async function (searchParams?: LeadStatusSearchReq) {
    const response = await axiosInstance.request({
      url: "/LeadStatuses/search",
      method: "GET",
      params: searchParams,
    });

    return response.data;
  },
};
