import {
  AddRoleReq,
  RemoveRoleReq,
  SearchUsersReq,
  StaffEditReq,
  UserCreateReq,
  VerifyEmailReq,
} from "../models/User";

import { axiosInstance } from "../provider";

export const UserApi = {
  search: async function (data: SearchUsersReq) {
    const response = await axiosInstance.request({
      url: `/Users/search`,
      method: "GET",
      params: data,
    });

    return response.data;
  },
  createUser: async function (data: UserCreateReq) {
    const response = await axiosInstance.request({
      url: "/Users",
      method: "POST",
      data: data,
    });

    return response.data;
  },
  updateStaff: async function (email?: string, data?: StaffEditReq) {
    const response = await axiosInstance.request({
      url: "/Users/" + email,
      method: "PUT",
      data: data,
    });

    return response.data;
  },
  addRoleToUser: async function (data: AddRoleReq) {
    const response = await axiosInstance.request({
      url: "/Users/add-role",
      method: "POST",
      data: data,
    });

    return response.data;
  },
  removeRoleFromUser: async function (data: RemoveRoleReq) {
    const response = await axiosInstance.request({
      url: "/Users/remove-role",
      method: "DELETE",
      data: data,
    });

    return response.data;
  },
  deleteUser: async function (username: string) {
    const response = await axiosInstance.request({
      url: `/Users/` + username,
      method: "DELETE",
    });

    return response.data;
  },
  getUserByName: async function (username?: string) {
    const response = await axiosInstance.request({
      url: `/Users/get/` + username,
      method: "GET",
    });

    return response.data;
  },
  getAllRoles: async function () {
    const response = await axiosInstance.request({
      url: `/Users/roles`,
      method: "GET",
    });

    return response.data;
  },
  updateProfilePicture: async function (email?: string, data?: FormData) {
    const response = await axiosInstance.request({
      url: "/Users/" + email + "/update-profile-picture",
      method: "POST",
      data: data,
      headers: { "Content-Type": "multipart/form-data" },
    });

    return response.data;
  },
};
