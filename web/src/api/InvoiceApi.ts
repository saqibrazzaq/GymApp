import {
  InvoiceEditReq,
  InvoiceItemEditReq,
  InvoiceItemSearchReq,
  InvoiceSearchReq,
} from "../dtos/Invoice";
import { axiosInstance } from "../provider";

export const InvoiceApi = {
  create: async function (data?: InvoiceEditReq) {
    const response = await axiosInstance.request({
      url: "/Invoices",
      method: "POST",
      data: data,
    });

    return response.data;
  },
  createItem: async function (data?: InvoiceItemEditReq) {
    const response = await axiosInstance.request({
      url: "/Invoices/items",
      method: "POST",
      data: data,
    });

    return response.data;
  },
  update: async function (invoiceId?: string, data?: InvoiceEditReq) {
    const response = await axiosInstance.request({
      url: "/Invoices/" + invoiceId,
      method: "PUT",
      data: data,
    });

    return response.data;
  },
  updateItem: async function (invoiceItemId?: string, data?: InvoiceItemEditReq) {
    const response = await axiosInstance.request({
      url: "/Invoices/items/" + invoiceItemId,
      method: "PUT",
      data: data,
    });

    return response.data;
  },
  delete: async function (invoiceId?: string) {
    const response = await axiosInstance.request({
      url: `/Invoices/` + invoiceId,
      method: "DELETE",
    });

    return response.data;
  },
  deleteItem: async function (invoiceItemId?: string) {
    const response = await axiosInstance.request({
      url: `/Invoices/items/` + invoiceItemId,
      method: "DELETE",
    });

    return response.data;
  },
  get: async function (invoiceId?: string) {
    const response = await axiosInstance.request({
      url: `/Invoices/` + invoiceId,
      method: "GET",
    });

    return response.data;
  },
  getItem: async function (invoiceItemId?: string) {
    const response = await axiosInstance.request({
      url: `/Invoices/items/` + invoiceItemId,
      method: "GET",
    });

    return response.data;
  },
  search: async function (searchParams?: InvoiceSearchReq) {
    const response = await axiosInstance.request({
      url: "/Invoices/search",
      method: "GET",
      params: searchParams,
    });

    return response.data;
  },
  searchItems: async function (searchParams?: InvoiceItemSearchReq) {
    const response = await axiosInstance.request({
      url: "/Invoices/items/search",
      method: "GET",
      params: searchParams,
    });

    return response.data;
  },
};
