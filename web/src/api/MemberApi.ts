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

export const MemberApi = {
  search: async function (data: SearchUsersReq) {
    const response = await axiosInstance.request({
      url: `/Members/search`,
      method: "GET",
      params: data,
    });

    return response.data;
  },
  createMember: async function (data: UserCreateReq) {
    const response = await axiosInstance.request({
      url: "/Members",
      method: "POST",
      data: data,
    });

    return response.data;
  },
  updateMember: async function (email?: string, data?: StaffEditReq) {
    const response = await axiosInstance.request({
      url: "/Members/" + email,
      method: "PUT",
      data: data,
    });

    return response.data;
  },
  deleteMember: async function (username: string) {
    const response = await axiosInstance.request({
      url: `/Members/` + username,
      method: "DELETE",
    });

    return response.data;
  },
  getUserByName: async function (username?: string) {
    const response = await axiosInstance.request({
      url: `/Members/get/` + username,
      method: "GET",
    });

    return response.data;
  },
  updateProfilePicture: async function (email?: string, data?: FormData) {
    const response = await axiosInstance.request({
      url: "/Members/" + email + "/update-profile-picture",
      method: "POST",
      data: data,
      headers: { "Content-Type": "multipart/form-data" },
    });

    return response.data;
  },
  setNewPassword: async function (email?: string, data?: SetNewPasswordReq) {
    const response = await axiosInstance.request({
      url: "/Members/" + email + "/set-new-password",
      method: "POST",
      data: data,
    });

    return response.data;
  },
};
