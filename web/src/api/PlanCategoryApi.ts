import { PlanCategoryEditReq, PlanCategorySearchReq } from "../models/Plan";
import { axiosInstance } from "../provider";

export const PlanCategoryApi = {
  create: async function (data?: PlanCategoryEditReq) {
    const response = await axiosInstance.request({
      url: "/PlanCategories",
      method: "POST",
      data: data,
    });

    return response.data;
  },
  update: async function (planCategoryId?: string, data?: PlanCategoryEditReq) {
    const response = await axiosInstance.request({
      url: "/PlanCategories/" + planCategoryId,
      method: "PUT",
      data: data,
    });

    return response.data;
  },
  delete: async function (planCategoryId?: string) {
    const response = await axiosInstance.request({
      url: `/PlanCategories/` + planCategoryId,
      method: "DELETE",
    });

    return response.data;
  },
  get: async function (planCategoryId?: string) {
    const response = await axiosInstance.request({
      url: `/PlanCategories/` + planCategoryId,
      method: "GET",
    });

    return response.data;
  },
  search: async function (searchParams?: PlanCategorySearchReq) {
    const response = await axiosInstance.request({
      url: "/PlanCategories/search",
      method: "GET",
      params: searchParams,
    });

    return response.data;
  },
};
