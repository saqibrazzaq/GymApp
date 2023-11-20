import React, { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { Route, Routes } from "react-router-dom";
import { setLoggedInUser } from "./storage/Redux/userAuthSlice";
import { toastNotify } from "./Helper";
import { Footer, Header } from "./layout";
import { Home } from "./pages/Home";
import { AccessDenied, ForgotPassword, Login, Register, ResetPassword } from "./pages/Auth";
import { Private, Public } from "./pages/ZOther";
import { AccountLayout } from "./layout/AccountLayout";
import { AdminLayout } from "./layout/AdminLayout";
import { SuperAdminLayout } from "./layout/SuperAdminLayout";
import { MyProfileApi } from "./api";

function App() {
  const dispatch = useDispatch();
  useEffect(() => {
    const localToken = localStorage.getItem("token");
    if (localToken) {
      MyProfileApi.userInfo()
        .then((res) => {
          // console.log("In App.tsx");
          // console.log(res);
          dispatch(setLoggedInUser(res));
        })
        .catch((error) => toastNotify(error.Message));
    }
  }, []);
  return (
    <div>
      <Header />
      <div className="pb-5">
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/login" element={<Login />} />
          <Route path="/register" element={<Register />} />
          <Route path="/forgot-password" element={<ForgotPassword />} />
          <Route path="/reset-password" element={<ResetPassword />} />
          <Route path="/public" element={<Public />} />
          <Route path="/private" element={<Private />} />
          <Route path="/account/*" element={<AccountLayout />} />
          <Route path="/admin/*" element={<AdminLayout />} />
          <Route path="/superadmin/*" element={<SuperAdminLayout />} />
          <Route path="/access-denied" element={<AccessDenied />} />
        </Routes>
      </div>
      <Footer />
    </div>
  );
}

export default App;
