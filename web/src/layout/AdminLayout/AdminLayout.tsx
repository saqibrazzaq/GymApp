import { Box, Center, Flex, Square, Stack, Text } from "@chakra-ui/react";
import React from "react";
import { Outlet, Route, Routes } from "react-router-dom";
import { FiCompass, FiHome, FiTrendingUp } from "react-icons/fi";
import LeftSideMenu, { LinkItemProps } from "./LeftSideMenu";
import { MdOutlineVerifiedUser, MdPerson } from "react-icons/md";
import { AiOutlineUnlock } from "react-icons/ai";
import { AdminHome, DeleteUser, UpdateUser, UserRoles, Users } from "../../pages/Admin";
import { NotFound } from "../../pages/ZOther";
import { withAdminAuth } from "../../hoc";
import { BusinessLogo, SettingsHome } from "../../pages/Admin/Settings";
import { PlanCategories, PlanCategoryDelete, PlanCategoryEdit } from "../../pages/Plans/Category";

const LinkItems: Array<LinkItemProps> = [
  { name: "Home", icon: FiHome, href: "/admin" },
  {
    name: "People",
    icon: AiOutlineUnlock,
    href: "/admin/users",
    children: [
      {
        name: "Members",
        icon: AiOutlineUnlock,
        href: "/admin/users",
      },
      {
        name: "Staff",
        icon: AiOutlineUnlock,
        href: "/admin/users",
      },
    ],
  },
  {
    name: "Settings",
    icon: MdOutlineVerifiedUser,
    href: "/admin/settings",
    children: [
      {
        name: "Settings",
        icon: MdOutlineVerifiedUser,
        href: "/admin/settings",
      },
    ],
  },
  {
    name: "Plans",
    icon: MdPerson,
    href: "/admin/3",
    children: [
      {
        name: "Plans",
        icon: MdOutlineVerifiedUser,
        href: "/admin/plans",
      },
      {
        name: "Categories",
        icon: MdOutlineVerifiedUser,
        href: "/admin/plans/categories",
      },
    ],
  },
];

const AdminLayout = () => {
  return (
    <Flex mt="2">
      <Box w="250px">
        <LeftSideMenu menuItems={LinkItems} />
      </Box>
      <Center bg="gray.300" w="1px"></Center>
      <Box flex="1">
        <Routes>
          <Route path="/" element={<AdminHome />} />
          <Route path="/users" element={<Users />} />
          <Route path="/users/update" element={<UpdateUser />} />
          <Route path="/users/:username/delete" element={<DeleteUser />} />
          <Route path="/users/:username/roles" element={<UserRoles />} />
          <Route path="/settings" element={<SettingsHome />} />
          <Route path="/settings/logo" element={<BusinessLogo />} />
          <Route path="/plans/categories" element={<PlanCategories />} />
          <Route path="/plans/categories/edit" element={<PlanCategoryEdit />} />
          <Route path="/plans/categories/:planCategoryId/edit" element={<PlanCategoryEdit />} />
          <Route path="/plans/categories/:planCategoryId/delete" element={<PlanCategoryDelete />} />
          <Route path="*" element={<NotFound />} />
        </Routes>
      </Box>
    </Flex>
  );
};

export default withAdminAuth(AdminLayout);
