import {
  AddRoleReq,
  RemoveRoleReq,
  SearchUsersReq,
  SetNewPasswordReq,
  StaffEditReq,
  UserCreateReq,
  VerifyEmailReq,
} from "../dtos/User";

import { axiosInstance } from "../provider";

export const StaffApi = {
  search: async function (data: SearchUsersReq) {
    const response = await axiosInstance.request({
      url: `/Staff/search`,
      method: "GET",
      params: data,
    });

    return response.data;
  },
  createUser: async function (data: UserCreateReq) {
    const response = await axiosInstance.request({
      url: "/Staff",
      method: "POST",
      data: data,
    });

    return response.data;
  },
  updateStaff: async function (email?: string, data?: StaffEditReq) {
    const response = await axiosInstance.request({
      url: "/Staff/" + email,
      method: "PUT",
      data: data,
    });

    return response.data;
  },
  addRoleToUser: async function (data: AddRoleReq) {
    const response = await axiosInstance.request({
      url: "/Staff/add-role",
      method: "POST",
      data: data,
    });

    return response.data;
  },
  removeRoleFromUser: async function (data: RemoveRoleReq) {
    const response = await axiosInstance.request({
      url: "/Staff/remove-role",
      method: "DELETE",
      data: data,
    });

    return response.data;
  },
  deleteUser: async function (username: string) {
    const response = await axiosInstance.request({
      url: `/Staff/` + username,
      method: "DELETE",
    });

    return response.data;
  },
  getUserByName: async function (username?: string) {
    const response = await axiosInstance.request({
      url: `/Staff/get/` + username,
      method: "GET",
    });

    return response.data;
  },
  getAllRoles: async function () {
    const response = await axiosInstance.request({
      url: `/Staff/roles`,
      method: "GET",
    });

    return response.data;
  },
  updateProfilePicture: async function (email?: string, data?: FormData) {
    const response = await axiosInstance.request({
      url: "/Staff/" + email + "/update-profile-picture",
      method: "POST",
      data: data,
      headers: { "Content-Type": "multipart/form-data" },
    });

    return response.data;
  },
  setNewPassword: async function (email?: string, data?: SetNewPasswordReq) {
    const response = await axiosInstance.request({
      url: "/Staff/" + email + "/set-new-password",
      method: "POST",
      data: data,
    });

    return response.data;
  },
};
