import React, { useEffect } from "react";
import { WeatherApi } from "../../api/WeatherApi";
import { toastNotify } from "../../Helper";
import { MyProfileApi } from "../../api";

const Private = () => {
  useEffect(() => {
    MyProfileApi.userInfo()
      .then((res) => {
        console.log(res);
      })
      .catch((error) => {
        toastNotify(error.message, "error");
      });
  }, []);
  return <div>Private</div>;
};

export default Private;
