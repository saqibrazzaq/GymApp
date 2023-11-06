import { AccountEditReq } from "../models/Account";
import { axiosInstance } from "../provider";

export const AccountApi = {
  update: async function (data?: AccountEditReq) {
    const response = await axiosInstance.request({
      url: "/Accounts/",
      method: "PUT",
      data: data,
    });

    return response.data;
  },
  get: async function () {
    const response = await axiosInstance.request({
      url: `/Accounts/my`,
      method: "GET",
    });

    return response.data;
  },
  updateLogo: async function (data: FormData) {
    const response = await axiosInstance.request({
      url: "/Accounts/update-logo",
      method: "POST",
      data: data,
      headers: { "Content-Type": "multipart/form-data" },
    });

    return response.data;
  },
};
