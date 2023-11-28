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
import { StaffApi } from "../../../api/StaffApi";
import { toastNotify } from "../../../Helper";
import { SubmitButton } from "../../../components/Buttons";
import { ErrorDetails } from "../../../dtos/Error";
import { GenderRes, UserCreateReq } from "../../../dtos/User";
import { GenderDropdown } from "../../../components/Dropdowns";

YupPassword(Yup); // extend yup

const StaffEdit = () => {
  const params = useParams();
  const username = params.username;
  const updateText = username ? "Update User" : "Create User";
  const navigate = useNavigate();
  const [user, setUser] = useState<UserCreateReq>(new UserCreateReq());
  const [gender, setGender] = useState<GenderRes>();

  useEffect(() => {
    loadUser();
  }, []);

  const loadUser = () => {
    if (!username) return;
    StaffApi.getUserByName(username)
      .then((res) => {
        // console.log(res);
        setUser(res);
        setGender(res.gender);
      })
      .catch((err) => {
        let errDetails: ErrorDetails = err?.response?.data;
        toastNotify(errDetails?.Message ?? "Error", "error");
      });
  };

  // Formik validation schema
  const validationSchema = Yup.object({
    fullName: Yup.string().required("Full Name is required."),
    genderId: Yup.string(),
  });

  const convertEmptyStringToNull = (obj: UserCreateReq) => {
    obj.genderId = obj.genderId == "" ? undefined : obj.genderId;
    return obj;
  };

  const submitForm = (values: UserCreateReq) => {
    values = convertEmptyStringToNull(values);
    // console.log(values);
    StaffApi.updateStaff(username, values)
      .then((res) => {
        // console.log("New Admin user created successfully.");
        toastNotify("Staff updated successfully");
        navigate(-1);
      })
      .catch((err) => {
        let errDetails: ErrorDetails = err?.response?.data;
        // console.log("Error: " + err?.response?.data?.Message);
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
        {({ handleSubmit, errors, touched, setFieldValue }) => (
          <form onSubmit={handleSubmit}>
            <Stack spacing={4} as={Container} maxW={"3xl"}>
              <Heading fontSize={"xl"}>{updateText}</Heading>
              <FormControl isInvalid={!!errors.fullName && touched.fullName}>
                <FormLabel htmlFor="fullName">Full Name</FormLabel>
                <Field as={Input} id="fullName" name="fullName" type="text" />
                <FormErrorMessage>{errors.fullName}</FormErrorMessage>
              </FormControl>
              <FormControl isInvalid={!!errors.genderId && touched.genderId}>
                <FormLabel htmlFor="genderId">Gender</FormLabel>
                <Field as={Input} id="genderId" name="genderId" type="hidden" />
                <FormErrorMessage>{errors.genderId}</FormErrorMessage>

                <GenderDropdown
                  selectedGender={gender}
                  handleChange={(newValue?: GenderRes) => {
                    setFieldValue("genderId", newValue?.genderId ?? "");
                    setGender(newValue);
                  }}
                ></GenderDropdown>
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
