import { jwtDecode } from "jwt-decode";
import { Roles } from "../dtos/User/Roles";

const withSuperAdminAuth = (WrappedComponent: any) => {
  return (props: any) => {
    const accessToken = localStorage.getItem("token") ?? "";

    if (accessToken) {
      const decode: {
        roles: string;
      } = jwtDecode(accessToken);
      let arrRoles = decode.roles.split(",");
      // console.log(arrRoles);
      if (arrRoles?.indexOf(Roles.SuperAdmin) >= 0) {
        // console.log("SuperAdmin is logged in");
        return <WrappedComponent {...props} />;
      }
    } else {
      window.location.replace("/login");
      return null;
    }

    window.location.replace("/access-denied");
    return null;
  };
};

export default withSuperAdminAuth;
