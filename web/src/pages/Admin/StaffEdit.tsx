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
import { useEffect, useState } from "react";
import { Field, Formik } from "formik";
import { UserApi } from "../../api/UserApi";
import { toastNotify } from "../../Helper";
import { SubmitButton } from "../../components/Buttons";
import { ErrorDetails } from "../../models/Error";
import { UserCreateReq } from "../../models/User";

YupPassword(Yup); // extend yup

const StaffEdit = () => {
  const params = useParams();
  const username = params.username;
  const updateText = username ? "Update User" : "Create User";
  const [error, setError] = useState("");
  const [success, setSuccess] = useState("");
  const navigate = useNavigate();
  const [user, setUser] = useState<UserCreateReq>(new UserCreateReq());

  useEffect(() => {
    loadUser();
  }, []);

  const loadUser = () => {
    if (!username) return;
    UserApi.getUserByName(username)
      .then((res) => {
        console.log(res);
        setUser(res);
      })
      .catch((err) => {
        let errDetails: ErrorDetails = err?.response?.data;
        toastNotify(errDetails?.Message ?? "Error", "error");
      });
  };

  // Formik validation schema
  const validationSchema = Yup.object({
    fullName: Yup.string().required("Full Name is required."),
  });

  const submitForm = (values: UserCreateReq) => {
    setError("");
    setSuccess("");
    console.log(values);
    UserApi.updateStaff(username, values)
      .then((res) => {
        // console.log("New Admin user created successfully.");
        setSuccess("User created successfully.");
        toastNotify("Staff updated successfully");
        navigate(-1);
      })
      .catch((err) => {
        let errDetails: ErrorDetails = err?.response?.data;
        // console.log("Error: " + err?.response?.data?.Message);
        setError(errDetails?.Message || "User service failed.");
        toastNotify(errDetails?.Message || "User service failed.", "error");
      });
  };

  return (
    <Box p={4}>
      <Formik
        initialValues={user}
        onSubmit={(values) => {
          submitForm(values);
        }}
        validationSchema={validationSchema}
        enableReinitialize={true}
      >
        {({ handleSubmit, errors, touched }) => (
          <form onSubmit={handleSubmit}>
            <Stack spacing={4} as={Container} maxW={"3xl"}>
              <Heading fontSize={"xl"}>{updateText}</Heading>
              {error && (
                <Alert status="error">
                  <AlertIcon />
                  <AlertTitle>Create user failed</AlertTitle>
                  <AlertDescription>{error}</AlertDescription>
                </Alert>
              )}
              {success && (
                <Alert status="success">
                  <AlertIcon />
                  <AlertTitle>User created</AlertTitle>
                  <AlertDescription>{success}</AlertDescription>
                </Alert>
              )}
              <FormControl isInvalid={!!errors.fullName && touched.fullName}>
                <FormLabel htmlFor="fullName">Full Name</FormLabel>
                <Field as={Input} id="fullName" name="fullName" type="text" />
                <FormErrorMessage>{errors.fullName}</FormErrorMessage>
              </FormControl>
              <Stack spacing={6}>
                <SubmitButton text={updateText} />
              </Stack>
            </Stack>
          </form>
        )}
      </Formik>
    </Box>
  );
};

export default StaffEdit;
