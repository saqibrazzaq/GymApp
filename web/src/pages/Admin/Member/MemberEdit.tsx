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
import { toastNotify } from "../../../Helper";
import { SubmitButton } from "../../../components/Buttons";
import { ErrorDetails } from "../../../dtos/Error";
import { UserCreateReq } from "../../../dtos/User";
import { MemberApi } from "../../../api";

YupPassword(Yup); // extend yup

const MemberEdit = () => {
  const params = useParams();
  const username = params.username;
  const updateText = username ? "Update Member" : "Create Member";
  const navigate = useNavigate();
  const [user, setUser] = useState<UserCreateReq>(new UserCreateReq());

  useEffect(() => {
    loadMember();
  }, []);

  const loadMember = () => {
    if (!username) return;
    MemberApi.getUserByName(username)
      .then((res) => {
        // console.log(res);
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
    // console.log(values);
    MemberApi.updateMember(username, values)
      .then((res) => {
        // console.log("New Admin user created successfully.");
        toastNotify("Member updated successfully");
        navigate(-1);
      })
      .catch((err) => {
        let errDetails: ErrorDetails = err?.response?.data;
        // console.log("Error: " + err?.response?.data?.Message);
        toastNotify(errDetails?.Message || "Service failed.", "error");
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

export default MemberEdit;
