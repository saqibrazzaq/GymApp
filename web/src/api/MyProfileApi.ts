import {
  ChangePasswordReq,
  LoginReq,
  ResetPasswordReq,
  SendForgotPasswordEmailReq,
  TokenRes,
  UserCreateReq,
  VerifyEmailReq,
} from "../dtos/User";
import { axiosInstance } from "../provider";

export const MyProfileApi = {
  userInfo: async function () {
    const response = await axiosInstance.request({
      url: `/MyProfile/info`,
      method: "GET",
    });

    return response.data;
  },
  changePassword: async function (data: ChangePasswordReq) {
    const response = await axiosInstance.request({
      url: "/MyProfile/change-password",
      method: "POST",
      data: data,
    });

    return response.data;
  },
  verifyEmail: async function (data: VerifyEmailReq) {
    const response = await axiosInstance.request({
      url: "/MyProfile/verify-email",
      method: "POST",
      data: data,
    });

    return response.data;
  },
  sendVerificationEmail: async function () {
    const response = await axiosInstance.request({
      url: `/MyProfile/send-verification-email`,
      method: "GET",
    });

    return response.data;
  },
  updateProfilePicture: async function (data: FormData) {
    const response = await axiosInstance.request({
      url: "/MyProfile/update-profile-picture",
      method: "POST",
      data: data,
      headers: { "Content-Type": "multipart/form-data" },
    });

    return response.data;
  },
};
