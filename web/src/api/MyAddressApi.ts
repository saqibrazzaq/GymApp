import { AddressEditReq } from "../models/Address";
import { axiosInstance } from "../provider";

export const MyAddressApi = {
  create: async function (data?: AddressEditReq) {
    const response = await axiosInstance.request({
      url: "/MyAddresses",
      method: "POST",
      data: data,
    });

    return response.data;
  },
  update: async function (userAddressId?: string, data?: AddressEditReq) {
    const response = await axiosInstance.request({
      url: "/MyAddresses/" + userAddressId,
      method: "PUT",
      data: data,
    });

    return response.data;
  },
  delete: async function (userAddressId?: string) {
    const response = await axiosInstance.request({
      url: `/MyAddresses/` + userAddressId,
      method: "DELETE",
    });

    return response.data;
  },
  get: async function (userAddressId?: string) {
    const response = await axiosInstance.request({
      url: `/MyAddresses/` + userAddressId,
      method: "GET",
    });

    return response.data;
  },
  getAll: async function () {
    const response = await axiosInstance.request({
      url: `/MyAddresses/all`,
      method: "GET",
    });

    return response.data;
  },
};
