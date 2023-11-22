import { PlanEditReq, PlanSearchReq } from "../dtos/Plan";
import { axiosInstance } from "../provider";

export const PlanApi = {
  create: async function (data?: PlanEditReq) {
    const response = await axiosInstance.request({
      url: "/Plans",
      method: "POST",
      data: data,
    });

    return response.data;
  },
  update: async function (planId?: string, data?: PlanEditReq) {
    const response = await axiosInstance.request({
      url: "/Plans/" + planId,
      method: "PUT",
      data: data,
    });

    return response.data;
  },
  delete: async function (planId?: string) {
    const response = await axiosInstance.request({
      url: `/Plans/` + planId,
      method: "DELETE",
    });

    return response.data;
  },
  get: async function (planId?: string) {
    const response = await axiosInstance.request({
      url: `/Plans/` + planId,
      method: "GET",
    });

    return response.data;
  },
  search: async function (searchParams?: PlanSearchReq) {
    const response = await axiosInstance.request({
      url: "/Plans/search",
      method: "GET",
      params: searchParams,
    });

    return response.data;
  },
};
