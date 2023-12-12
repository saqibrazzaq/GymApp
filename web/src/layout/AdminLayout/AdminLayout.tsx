import { Box, Center, Flex, Square, Stack, Text } from "@chakra-ui/react";
import React from "react";
import { Outlet, Route, Routes } from "react-router-dom";
import { FiCompass, FiHome, FiTrendingUp } from "react-icons/fi";
import LeftSideMenu, { LinkItemProps } from "./LeftSideMenu";
import { MdOutlineVerifiedUser, MdPerson } from "react-icons/md";
import { AiOutlineUnlock } from "react-icons/ai";
import {
  AdminHome,
  StaffDelete,
  StaffProfilePicture,
  StaffSetNewPassword,
  StaffCreate,
  StaffEdit,
  StaffRoles,
  Staff,
} from "../../pages/Staff";
import { NotFound } from "../../pages/ZOther";
import { withAdminAuth } from "../../hoc";
import { BusinessLogo, SettingsHome } from "../../pages/Admin/Settings";
import { PlanCategories, PlanCategoryDelete, PlanCategoryEdit } from "../../pages/Plans/Category";
import { PlanDelete, PlanEdit, Plans } from "../../pages/Plans/Plan";
import { AddressDelete, AddressEdit, Addresses } from "../../pages/Address";
import { MemberCreate, MemberDelete, MemberEdit, Members } from "../../pages/Member";
import { SubscriptionDelete, SubscriptionEdit, Subscriptions } from "../../pages/Subscription";

const LinkItems: Array<LinkItemProps> = [
  { name: "Home", icon: FiHome, href: "/admin" },
  {
    name: "People",
    icon: AiOutlineUnlock,
    href: "",
    children: [
      {
        name: "Subscriptions",
        icon: AiOutlineUnlock,
        href: "/admin/subscriptions",
      },
      {
        name: "Members",
        icon: AiOutlineUnlock,
        href: "/admin/members",
      },
      {
        name: "Staff",
        icon: AiOutlineUnlock,
        href: "/admin/staff",
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
          <Route path="/staff" element={<Staff />} />
          <Route path="/staff/create" element={<StaffCreate />} />
          <Route path="/staff/:username/edit" element={<StaffEdit />} />
          <Route path="/staff/:username/delete" element={<StaffDelete />} />
          <Route path="/staff/:username/roles" element={<StaffRoles />} />
          <Route path="/members" element={<Members />} />
          <Route path="/members/create" element={<MemberCreate />} />
          <Route path="/members/:username/edit" element={<MemberEdit />} />
          <Route path="/members/:username/delete" element={<MemberDelete />} />
          <Route path="/subscriptions" element={<Subscriptions />} />
          <Route path="/subscriptions/edit" element={<SubscriptionEdit />} />
          <Route path="/subscriptions/:subscriptionId/edit" element={<SubscriptionEdit />} />
          <Route path="/subscriptions/:subscriptionId/delete" element={<SubscriptionDelete />} />
          <Route path="/settings" element={<SettingsHome />} />
          <Route path="/settings/logo" element={<BusinessLogo />} />
          <Route path="/plans/categories" element={<PlanCategories />} />
          <Route path="/plans/categories/edit" element={<PlanCategoryEdit />} />
          <Route path="/plans/categories/:planCategoryId/edit" element={<PlanCategoryEdit />} />
          <Route path="/plans/categories/:planCategoryId/delete" element={<PlanCategoryDelete />} />
          <Route path="/plans" element={<Plans />} />
          <Route path="/plans/edit" element={<PlanEdit />} />
          <Route path="/plans/:planId/edit" element={<PlanEdit />} />
          <Route path="/plans/:planId/delete" element={<PlanDelete />} />
          <Route path="/:email/addresses" element={<Addresses />} />
          <Route path="/:email/profile-picture" element={<StaffProfilePicture />} />
          <Route path="/:email/new-password" element={<StaffSetNewPassword />} />
          <Route path="/:email/addresses/edit" element={<AddressEdit />} />
          <Route path="/:email/addresses/:userAddressId/edit" element={<AddressEdit />} />
          <Route path="/:email/addresses/:userAddressId/delete" element={<AddressDelete />} />
          <Route path="*" element={<NotFound />} />
        </Routes>
      </Box>
    </Flex>
  );
};

export default withAdminAuth(AdminLayout);
