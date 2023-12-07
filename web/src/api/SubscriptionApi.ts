import { SubscriptionEditReq } from "../dtos/Subscription";
import SubscriptionSearchReq from "../dtos/Subscription/SubscriptionSearchReq";
import { axiosInstance } from "../provider";

export const SubscriptionApi = {
  create: async function (data?: SubscriptionEditReq) {
    const response = await axiosInstance.request({
      url: "/Subscriptions",
      method: "POST",
      data: data,
    });

    return response.data;
  },
  update: async function (subscriptionId?: string, data?: SubscriptionEditReq) {
    const response = await axiosInstance.request({
      url: "/Subscriptions/" + subscriptionId,
      method: "PUT",
      data: data,
    });

    return response.data;
  },
  delete: async function (subscriptionId?: string) {
    const response = await axiosInstance.request({
      url: `/Subscriptions/` + subscriptionId,
      method: "DELETE",
    });

    return response.data;
  },
  get: async function (subscriptionId?: string) {
    const response = await axiosInstance.request({
      url: `/Subscriptions/` + subscriptionId,
      method: "GET",
    });

    return response.data;
  },
  search: async function (searchParams?: SubscriptionSearchReq) {
    const response = await axiosInstance.request({
      url: "/Subscriptions/search",
      method: "GET",
      params: searchParams,
    });

    return response.data;
  },
};
