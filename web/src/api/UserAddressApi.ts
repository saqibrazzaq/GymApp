import { AddressEditReq } from "../dtos/Address";
import { axiosInstance } from "../provider";

export const UserAddressApi = {
  create: async function (email?: string, data?: AddressEditReq) {
    const response = await axiosInstance.request({
      url: "/UserAddresses/" + email,
      method: "POST",
      data: data,
    });

    return response.data;
  },
  update: async function (userAddressId?: string, data?: AddressEditReq) {
    const response = await axiosInstance.request({
      url: "/UserAddresses/" + userAddressId,
      method: "PUT",
      data: data,
    });

    return response.data;
  },
  delete: async function (userAddressId?: string) {
    const response = await axiosInstance.request({
      url: `/UserAddresses/` + userAddressId,
      method: "DELETE",
    });

    return response.data;
  },
  get: async function (userAddressId?: string) {
    const response = await axiosInstance.request({
      url: `/UserAddresses/` + userAddressId,
      method: "GET",
    });

    return response.data;
  },
  getAll: async function (email?: string) {
    const response = await axiosInstance.request({
      url: "/UserAddresses/" + email + "/all",
      method: "GET",
    });

    return response.data;
  },
};
