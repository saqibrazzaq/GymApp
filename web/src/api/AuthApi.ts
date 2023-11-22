import {
  ChangePasswordReq,
  LoginReq,
  ResetPasswordReq,
  SendForgotPasswordEmailReq,
  TokenRes,
  UserCreateReq,
} from "../dtos/User";
import { axiosInstance } from "../provider";

export const AuthApi = {
  register: async function (data: UserCreateReq) {
    const response = await axiosInstance.request({
      url: "/auth/register",
      method: "POST",
      data: data,
    });

    return response.data;
  },
  login: async function (data: LoginReq) {
    const response = await axiosInstance.request({
      url: "/auth/login",
      method: "POST",
      data: data,
    });

    return response.data;
  },
  refreshToken: async function (data: TokenRes) {
    const response = await axiosInstance.request({
      url: "/auth/refresh-token",
      method: "POST",
      data: data,
    });

    return response.data;
  },
  sendForgotPasswordEmail: async function (params: SendForgotPasswordEmailReq) {
    const response = await axiosInstance.request({
      url: `/auth/send-forgot-password-email`,
      method: "GET",
      params: params,
    });

    return response.data;
  },
  resetPassword: async function (data: ResetPasswordReq) {
    const response = await axiosInstance.request({
      url: "/auth/reset-password",
      method: "POST",
      data: data,
    });

    return response.data;
  },
};
