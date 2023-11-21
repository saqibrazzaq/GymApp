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
import { useNavigate, useParams } from "react-router-dom";
import { useState } from "react";
import { Field, Formik } from "formik";
import { SetNewPasswordReq } from "../../../models/User";
import { UserApi } from "../../../api";
import { toastNotify } from "../../../Helper";
import { ErrorDetails } from "../../../models/Error";
import { SubmitButton } from "../../../components/Buttons";

YupPassword(Yup); // extend yup

export default function SetNewPassword(): JSX.Element {
  const params = useParams();
  const email = params.email;
  const navigate = useNavigate();

  let pwdData = new SetNewPasswordReq();

  // Formik validation schema
  const validationSchema = Yup.object({
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

  const submitForm = (values: SetNewPasswordReq) => {
    // console.log(values);
    UserApi.setNewPassword(email, values)
      .then((res) => {
        // console.log("Password changed successfully.");
        toastNotify("New Password set successfully for " + email);
      })
      .catch((err) => {
        let errDetails: ErrorDetails = err?.response?.data;
        // console.log("Error: " + err?.response?.data?.Message);
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
        enableReinitialize={true}
      >
        {({ handleSubmit, errors, touched }) => (
          <form onSubmit={handleSubmit}>
            <Stack spacing={4} as={Container} maxW={"3xl"}>
              <Heading fontSize={"xl"}>Set New Password for {email}</Heading>
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
                <SubmitButton text="Set New Password" />
              </Stack>
            </Stack>
          </form>
        )}
      </Formik>
    </Box>
  );
}
