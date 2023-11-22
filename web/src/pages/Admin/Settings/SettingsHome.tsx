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
import { ErrorDetails } from "../../../dtos/Error";
import { StateRes } from "../../../dtos/Country";
import { CurrencyDropdown, StateDropdown } from "../../../components/Dropdowns";
import { AccountEditReq } from "../../../dtos/Account";
import { AccountApi } from "../../../api/AccountApi";
import { CancelButton, RegularButton } from "../../../components/Buttons";
import { CurrencyRes } from "../../../dtos/Currency";

const SettingsHome = () => {
  const params = useParams();
  const [account, setAccount] = useState<AccountEditReq>(new AccountEditReq());
  const [state, setState] = useState<StateRes>();
  const [currency, setCurrency] = useState<CurrencyRes>();
  const navigate = useNavigate();

  useEffect(() => {
    loadAccount();
  }, []);

  const loadAccount = () => {
    AccountApi.get()
      .then((res) => {
        // console.log(res);
        setAccount(res);
        setState(res.state);
        setCurrency(res.currency);
      })
      .catch((err) => {
        let errDetails: ErrorDetails = err?.response?.data;
        toastNotify(errDetails?.Message ?? "Error", "error");
      });
  };

  // Formik validation schema
  const validationSchema = Yup.object({
    logoUrl: Yup.string(),
    companyName: Yup.string(),
    companyWebsite: Yup.string(),
    companyEmail: Yup.string(),
    companyPhone: Yup.string(),
    address1: Yup.string(),
    address2: Yup.string(),
    city: Yup.string(),
    stateId: Yup.string(),
    businessLicense: Yup.string(),
    facebookPage: Yup.string(),
    facebookGroup: Yup.string(),
    twitterHandle: Yup.string(),
    instagramUsername: Yup.string(),
    currencyId: Yup.string(),
  });

  const submitForm = (values: AccountEditReq) => {
    values = convertEmptyStringToNull(values);
    // console.log("update: ");
    // console.log(values);
    updateAccount(values);
  };

  const convertEmptyStringToNull = (obj: AccountEditReq) => {
    obj.stateId = obj.stateId == "" ? undefined : obj.stateId;
    obj.currencyId = obj.currencyId == "" ? undefined : obj.currencyId;
    return obj;
  };

  const updateAccount = (values: AccountEditReq) => {
    AccountApi.update(values)
      .then((res) => {
        toastNotify("Settings updated successfully");
        navigate(-1);
      })
      .catch((err) => {
        let errDetails: ErrorDetails = err?.response?.data;
        toastNotify(errDetails?.Message ?? "Error", "error");
      });
  };

  const showUpdateForm = () => (
    <Box p={0}>
      <Formik
        initialValues={account}
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
                <Field as={Input} id="logoUrl" name="logoUrl" type="hidden" />
              </FormControl>
              <Heading size={"md"}>Contact</Heading>
              <Flex>
                <FormControl mr={5} isInvalid={!!errors.companyName && touched.companyName}>
                  <FormLabel fontSize={"sm"} htmlFor="companyName">
                    Company Name
                  </FormLabel>
                  <Field size={"sm"} as={Input} id="companyName" name="companyName" type="text" />
                  <FormErrorMessage>{errors.companyName}</FormErrorMessage>
                </FormControl>
                <FormControl isInvalid={!!errors.companyWebsite && touched.companyWebsite}>
                  <FormLabel fontSize={"sm"} htmlFor="companyWebsite">
                    Company Website
                  </FormLabel>
                  <Field
                    size={"sm"}
                    as={Input}
                    id="companyWebsite"
                    name="companyWebsite"
                    type="text"
                  />
                  <FormErrorMessage>{errors.companyWebsite}</FormErrorMessage>
                </FormControl>
              </Flex>
              <Flex>
                <FormControl mr={5} isInvalid={!!errors.companyEmail && touched.companyEmail}>
                  <FormLabel fontSize={"sm"} htmlFor="companyEmail">
                    Company Email
                  </FormLabel>
                  <Field size={"sm"} as={Input} id="companyEmail" name="companyEmail" type="text" />
                  <FormErrorMessage>{errors.companyEmail}</FormErrorMessage>
                </FormControl>
                <FormControl isInvalid={!!errors.companyPhone && touched.companyPhone}>
                  <FormLabel fontSize={"sm"} htmlFor="companyPhone">
                    Company Phone
                  </FormLabel>
                  <Field size={"sm"} as={Input} id="companyPhone" name="companyPhone" type="text" />
                  <FormErrorMessage>{errors.companyPhone}</FormErrorMessage>
                </FormControl>
              </Flex>
              <FormControl isInvalid={!!errors.address1 && touched.address1}>
                <FormLabel fontSize={"sm"} htmlFor="address1">
                  Address 1
                </FormLabel>
                <Field size={"sm"} as={Input} id="address1" name="address1" type="text" />
                <FormErrorMessage>{errors.address1}</FormErrorMessage>
              </FormControl>
              <FormControl isInvalid={!!errors.address2 && touched.address2}>
                <FormLabel fontSize={"sm"} htmlFor="address2">
                  Address 2
                </FormLabel>
                <Field size={"sm"} as={Input} id="address2" name="address2" type="text" />
                <FormErrorMessage>{errors.address2}</FormErrorMessage>
              </FormControl>
              <FormControl isInvalid={!!errors.city && touched.city}>
                <FormLabel fontSize={"sm"} htmlFor="city">
                  City
                </FormLabel>
                <Field size={"sm"} as={Input} id="city" name="city" type="text" />
                <FormErrorMessage>{errors.city}</FormErrorMessage>
              </FormControl>
              <FormControl isInvalid={!!errors.stateId && touched.stateId}>
                <FormLabel htmlFor="stateId">State</FormLabel>
                <Field as={Input} id="stateId" name="stateId" type="hidden" />
                <FormErrorMessage>{errors.stateId}</FormErrorMessage>

                <StateDropdown
                  selectedState={state}
                  handleChange={(newValue?: StateRes) => {
                    setFieldValue("stateId", newValue?.stateId ?? "");
                    setState(newValue);
                    // console.log(newValue);
                  }}
                ></StateDropdown>
              </FormControl>
              <Heading size={"md"}>Misc. Settings</Heading>
              <FormControl isInvalid={!!errors.businessLicense && touched.businessLicense}>
                <FormLabel fontSize={"sm"} htmlFor="businessLicense">
                  Business License
                </FormLabel>
                <Field
                  size={"sm"}
                  as={Input}
                  id="businessLicense"
                  name="businessLicense"
                  type="text"
                />
                <FormErrorMessage>{errors.businessLicense}</FormErrorMessage>
              </FormControl>
              <Heading size={"md"}>Social Settings</Heading>
              <Flex>
                <FormControl mr={5} isInvalid={!!errors.facebookPage && touched.facebookPage}>
                  <FormLabel fontSize={"sm"} htmlFor="facebookPage">
                    Facebook Page
                  </FormLabel>
                  <Field size={"sm"} as={Input} id="facebookPage" name="facebookPage" type="text" />
                  <FormErrorMessage>{errors.facebookPage}</FormErrorMessage>
                </FormControl>
                <FormControl isInvalid={!!errors.facebookGroup && touched.facebookGroup}>
                  <FormLabel fontSize={"sm"} htmlFor="facebookGroup">
                    Facebook Group
                  </FormLabel>
                  <Field
                    size={"sm"}
                    as={Input}
                    id="facebookGroup"
                    name="facebookGroup"
                    type="text"
                  />
                  <FormErrorMessage>{errors.facebookGroup}</FormErrorMessage>
                </FormControl>
              </Flex>
              <Flex>
                <FormControl mr={5} isInvalid={!!errors.twitterHandle && touched.twitterHandle}>
                  <FormLabel fontSize={"sm"} htmlFor="twitterHandle">
                    Twitter Handle
                  </FormLabel>
                  <Field
                    size={"sm"}
                    as={Input}
                    id="twitterHandle"
                    name="twitterHandle"
                    type="text"
                  />
                  <FormErrorMessage>{errors.twitterHandle}</FormErrorMessage>
                </FormControl>
                <FormControl isInvalid={!!errors.instagramUsername && touched.instagramUsername}>
                  <FormLabel fontSize={"sm"} htmlFor="instagramUsername">
                    Instagram Username
                  </FormLabel>
                  <Field
                    size={"sm"}
                    as={Input}
                    id="instagramUsername"
                    name="instagramUsername"
                    type="text"
                  />
                  <FormErrorMessage>{errors.instagramUsername}</FormErrorMessage>
                </FormControl>
              </Flex>
              <Heading size={"md"}>Financial Settings</Heading>
              <FormControl isInvalid={!!errors.currencyId && touched.currencyId}>
                <FormLabel htmlFor="currencyId">Currency</FormLabel>
                <Field as={Input} id="currencyId" name="currencyId" type="hidden" />
                <FormErrorMessage>{errors.currencyId}</FormErrorMessage>

                <CurrencyDropdown
                  selectedCurrency={currency}
                  handleChange={(newValue?: CurrencyRes) => {
                    setFieldValue("currencyId", newValue?.currencyId ?? "");
                    setCurrency(newValue);
                  }}
                ></CurrencyDropdown>
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
        <Button size={"sm"} type="button" colorScheme={"gray"} onClick={() => navigate(-1)}>
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
