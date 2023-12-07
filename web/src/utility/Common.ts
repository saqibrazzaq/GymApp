import { DateTime } from "luxon";

export default class Common {
  static readonly DEFAULT_PAGE_SIZE = 5;
  static readonly DEFAULT_FONT_SIZE = "sm";
  static readonly DEFAULT_PROFILE_PICTURE =
    "https://res.cloudinary.com/saqibrazzaq-com/image/upload/v1655799546/mypizzastore/profile/Circle-icons-profile.svg_pag9mo.png";
  static readonly CLOUDINARY_NAME = "saqibrazzaq-com";
  static readonly API_BASE_URL = "https://localhost:7179/api";

  static dateToString(date?: Date) {
    return date?.toISOString().substring(0, 19);
  }

  static formatDate(date?: string) {
    // console.log(DateTime.fromISO(date ?? "").toLocaleString())
    return DateTime.fromISO(date ?? "").toISODate();
  }
}
