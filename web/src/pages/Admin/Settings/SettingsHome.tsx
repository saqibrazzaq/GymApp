import {
  Box,
  Button,
  Container,
  Flex,
  FormControl,
  FormErrorMessage,
  FormLabel,
  Heading,
  Image,
  Input,
  Link,
  Spacer,
  Stack,
  Text,
  useToast,
} from "@chakra-ui/react";
import { useState, useEffect } from "react";
import { Link as RouteLink, useNavigate, useParams } from "react-router-dom";
import * as Yup from "yup";
import { Field, Formik } from "formik";
import { toastNotify } from "../../../Helper";
import { ErrorDetails } from "../../../models/Error";
import { StateRes } from "../../../models/Country";
import { StateDropdown } from "../../../components/Dropdowns";
import { AccountEditReq } from "../../../models/Account";
import { AccountApi } from "../../../api/AccountApi";
import { CancelButton, RegularButton } from "../../../components/Buttons";

const SettingsHome = () => {
  const params = useParams();
  const [account, setAccount] = useState<AccountEditReq>(new AccountEditReq());
  const [state, setState] = useState<StateRes>();
  const navigate = useNavigate();

  useEffect(() => {
    loadAccount();
  }, []);

  const loadAccount = () => {
    AccountApi.get()
      .then((res) => {
        // console.log(res);
        setAccount(res);
      })
      .catch((err) => {
        let errDetails: ErrorDetails = err?.response?.data;
        toastNotify(errDetails?.Message ?? "Error", "error");
      });
  };

  // Formik validation schema
  const validationSchema = Yup.object({
    logoUrl: Yup.string(),
  });

  const submitForm = (values: AccountEditReq) => {
    values = convertEmptyStringToNull(values);
    updateAccount(values);
  };

  const updateAccount = (values: AccountEditReq) => {
    AccountApi.update(values)
      .then((res) => {
        toastNotify("Settings updated successfully");
        loadAccount();
      })
      .catch((err) => {
        let errDetails: ErrorDetails = err?.response?.data;
        toastNotify(errDetails?.Message ?? "Error", "error");
      });
  };

  const convertNullToEmptyString = (obj: AccountEditReq) => {
    // obj.stateId ??= "";
    return obj;
  };

  const convertEmptyStringToNull = (obj: AccountEditReq) => {
    // obj.stateId = obj.stateId == "" ? undefined : obj.stateId;
    return obj;
  };

  const showUpdateForm = () => (
    <Box p={0}>
      <Formik
        initialValues={convertNullToEmptyString(account)}
        onSubmit={(values) => {
          submitForm(values);
        }}
        validationSchema={validationSchema}
        enableReinitialize={true}
      >
        {({ handleSubmit, errors, touched, setFieldValue }) => (
          <form onSubmit={handleSubmit}>
            <Stack spacing={4} as={Container} maxW={"3xl"}>
              <FormControl>
                {account.logoUrl ? (
                  <Image boxSize={100} src={account.logoUrl} />
                ) : (
                  <Text>No logo</Text>
                )}
                <Link as={RouteLink} to={"logo"}>
                  <CancelButton text="Upload New Logo" />
                </Link>
              </FormControl>

              <Stack direction={"row"} spacing={6}>
                <Button size={"sm"} type="submit" colorScheme={"blue"}>
                  {"Update Settings"}
                </Button>
              </Stack>
            </Stack>
          </form>
        )}
      </Formik>
    </Box>
  );

  const displayHeading = () => (
    <Flex>
      <Box>
        <Heading fontSize={"lg"}>{"Settings"}</Heading>
      </Box>
      <Spacer />
      <Box>
        <Button
          size={"sm"}
          type="button"
          colorScheme={"gray"}
          onClick={() => navigate(-1)}
        >
          Back
        </Button>
      </Box>
    </Flex>
  );

  return (
    <Box width={"lg"} p={4}>
      <Stack spacing={4} as={Container} maxW={"3xl"}>
        {displayHeading()}
        {showUpdateForm()}
      </Stack>
    </Box>
  );
};

export default SettingsHome;
