import { jwtDecode } from "jwt-decode";
import { Roles } from "../dtos/User/Roles";

const withAdminAuth = (WrappedComponent: any) => {
  return (props: any) => {
    const accessToken = localStorage.getItem("token") ?? "";

    if (accessToken) {
      const decode: {
        roles: string;
      } = jwtDecode(accessToken);
      let arrRoles = decode.roles.split(",");
      // console.log(arrRoles);
      if (
        arrRoles?.indexOf(Roles.SuperAdmin) >= 0 ||
        arrRoles?.indexOf(Roles.Admin) >= 0 ||
        arrRoles?.indexOf(Roles.Manager) >= 0 ||
        arrRoles?.indexOf(Roles.Owner) >= 0
      ) {
        // console.log("Admin is logged in");
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

export default withAdminAuth;
