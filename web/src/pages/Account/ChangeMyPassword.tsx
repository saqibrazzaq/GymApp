import {
  Alert,
  AlertDescription,
  AlertIcon,
  AlertTitle,
  Box,
  Button,
  Container,
  Flex,
  FormControl,
  FormErrorMessage,
  FormLabel,
  Heading,
  Input,
  Stack,
} from "@chakra-ui/react";
import * as Yup from "yup";
import YupPassword from "yup-password";
import { useNavigate } from "react-router-dom";
import { useState } from "react";
import { Field, Formik } from "formik";
import { ChangePasswordReq } from "../../models/User";
import { SubmitButton } from "../../components/Buttons";
import { MyProfileApi } from "../../api";
import { ErrorDetails } from "../../models/Error";
import { toastNotify } from "../../Helper";

YupPassword(Yup); // extend yup

export default function ChangeMyPassword(): JSX.Element {
  let pwdData = new ChangePasswordReq();

  // Formik validation schema
  const validationSchema = Yup.object({
    currentPassword: Yup.string()
      .required("Current Password is required.")
      .min(6, "Minimum 6 characters required.")
      .minUppercase(1, "At least one Upper Case letter required.")
      .minLowercase(1, "At least one lower case letter required.")
      .minNumbers(1, "At least one number required")
      .minSymbols(1, "At least one symbol required"),
    newPassword: Yup.string()
      .required("New Password is required.")
      .min(6, "Minimum 6 characters required.")
      .minUppercase(1, "At least one Upper Case letter required.")
      .minLowercase(1, "At least one lower case letter required.")
      .minNumbers(1, "At least one number required")
      .minSymbols(1, "At least one symbol required"),
    confirmNewPassword: Yup.string()
      .required("Confirm New Password is required.")
      .min(6, "Minimum 6 characters required.")
      .minUppercase(1, "At least one Upper Case letter required.")
      .minLowercase(1, "At least one lower case letter required.")
      .minNumbers(1, "At least one number required")
      .minSymbols(1, "At least one symbol required"),
  });

  const submitForm = (values: ChangePasswordReq) => {
    MyProfileApi.changePassword(values)
      .then((res) => {
        toastNotify("Password changed successfully.");
      })
      .catch((err) => {
        let errDetails: ErrorDetails = err?.response?.data;
        toastNotify(errDetails?.Message || "Login service failed.", "error");
      });
  };

  return (
    <Box p={4}>
      <Formik
        initialValues={pwdData}
        onSubmit={(values) => {
          submitForm(values);
        }}
        validationSchema={validationSchema}
      >
        {({ handleSubmit, errors, touched }) => (
          <form onSubmit={handleSubmit}>
            <Stack spacing={4} as={Container} maxW={"3xl"}>
              <Heading fontSize={"xl"}>Change Password</Heading>
              <FormControl isInvalid={!!errors.currentPassword && touched.currentPassword}>
                <FormLabel htmlFor="currentPassword">Current Password</FormLabel>
                <Field as={Input} id="currentPassword" name="currentPassword" type="password" />
                <FormErrorMessage>{errors.currentPassword}</FormErrorMessage>
              </FormControl>
              <FormControl isInvalid={!!errors.newPassword && touched.newPassword}>
                <FormLabel htmlFor="newPassword">New Password</FormLabel>
                <Field as={Input} id="newPassword" name="newPassword" type="password" />
                <FormErrorMessage>{errors.newPassword}</FormErrorMessage>
              </FormControl>
              <FormControl isInvalid={!!errors.confirmNewPassword && touched.confirmNewPassword}>
                <FormLabel htmlFor="confirmNewPassword">Confirm New Password</FormLabel>
                <Field
                  as={Input}
                  id="confirmNewPassword"
                  name="confirmNewPassword"
                  type="password"
                />
                <FormErrorMessage>{errors.confirmNewPassword}</FormErrorMessage>
              </FormControl>
              <Stack spacing={6}>
                <SubmitButton text="Change Password" />
              </Stack>
            </Stack>
          </form>
        )}
      </Formik>
    </Box>
  );
}
