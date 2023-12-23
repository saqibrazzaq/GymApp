import {
  Box,
  Button,
  Container,
  Flex,
  FormControl,
  FormErrorMessage,
  FormLabel,
  Heading,
  Input,
  InputGroup,
  InputLeftAddon,
  Link,
  Select,
  Spacer,
  Stack,
  useToast,
} from "@chakra-ui/react";
import { useState, useEffect } from "react";
import { Link as RouteLink, useNavigate, useParams } from "react-router-dom";
import * as Yup from "yup";
import { Field, Formik } from "formik";
import { toastNotify } from "../../Helper";
import { ErrorDetails } from "../../dtos/Error";
import { UserAddressRes, UserRes } from "../../dtos/User";
import { Common } from "../../utility";
import { InvoiceEditReq, InvoiceStatusRes } from "../../dtos/Invoice";
import { InvoiceApi } from "../../api/InvoiceApi";
import { StateRes } from "../../dtos/Country";
import { InvoiceStatusDropdown, MemberDropdown, StateDropdown } from "../../components/Dropdowns";
import { AddressRes } from "../../dtos/Address";
import { UserAddressApi } from "../../api";

const InvoiceEdit = () => {
  const params = useParams();
  const invoiceId = params.invoiceId;
  const updateText = invoiceId ? "Update Invoice" : "Create Invoice";
  const [invoice, setInvoice] = useState<InvoiceEditReq>(new InvoiceEditReq());
  const [state, setState] = useState<StateRes>();
  const [invoiceStatus, setInvoiceStatus] = useState<InvoiceStatusRes>();
  const [user, setUser] = useState<UserRes>();
  const [shippingAddress, setShippingAddress] = useState<AddressRes>();
  const [addresses, setAddresses] = useState<UserAddressRes[]>([]);
  const navigate = useNavigate();

  useEffect(() => {
    loadInvoice();
  }, []);

  useEffect(() => {
    loadAddresses(user?.email);
  }, [user?.email]);

  const loadAddresses = (email?: string) => {
    if (!email) return;
    UserAddressApi.getAll(email)
      .then((res) => {
        console.log(res);
        setAddresses(res);
      })
      .catch((err) => {
        let errDetails: ErrorDetails = err?.response?.data;
        toastNotify(errDetails?.Message ?? "Error", "error");
      });
  };

  const loadInvoice = () => {
    if (!invoiceId) return;
    InvoiceApi.get(invoiceId)
      .then((res) => {
        // console.log(res);
        setInvoice(res);
        setState(res.state);
        setInvoiceStatus(res.status);
        setUser(res.user);
      })
      .catch((err) => {
        let errDetails: ErrorDetails = err?.response?.data;
        toastNotify(errDetails?.Message ?? "Error", "error");
      });
  };

  // Formik validation schema
  const validationSchema = Yup.object({
    invoiceId: Yup.string(),
    email: Yup.string(),
    userId: Yup.string().required(),
    fullName: Yup.string(),
    phone: Yup.string(),
    address1: Yup.string(),
    address2: Yup.string(),
    city: Yup.string(),
    stateId: Yup.string(),
    issueDate: Yup.date().required(),
    statusId: Yup.string(),
  });

  const submitForm = (values: InvoiceEditReq) => {
    values = convertEmptyStringToNull(values);
    if (invoiceId) {
      updateInvoice(values);
    } else {
      createInvoice(values);
    }
  };

  const convertNullToEmptyString = (obj: InvoiceEditReq) => {
    obj.statusId ??= "";
    obj.stateId ??= "";
    obj.userId ??= "";
    obj.shippingAddressId ??= "";
    return obj;
  };

  const convertEmptyStringToNull = (obj: InvoiceEditReq) => {
    obj.statusId = obj.statusId == "" ? undefined : obj.statusId;
    obj.stateId = obj.stateId == "" ? undefined : obj.stateId;
    obj.userId = obj.userId == "" ? undefined : obj.userId;
    obj.shippingAddressId = obj.shippingAddressId == "" ? undefined : obj.shippingAddressId;
    return obj;
  };

  const updateInvoice = (values: InvoiceEditReq) => {
    InvoiceApi.update(invoiceId, values)
      .then((res) => {
        toastNotify("Invoice updated successfully");
        navigate(-1);
      })
      .catch((err) => {
        let errDetails: ErrorDetails = err?.response?.data;
        toastNotify(errDetails?.Message ?? "Error", "error");
      });
  };

  const createInvoice = (values: InvoiceEditReq) => {
    console.log(values);
    InvoiceApi.create(values)
      .then((res) => {
        toastNotify("Invoice created successfully");
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
        initialValues={convertNullToEmptyString(invoice)}
        onSubmit={(values) => {
          submitForm(values);
        }}
        validationSchema={validationSchema}
        enableReinitialize={true}
      >
        {({ handleSubmit, errors, touched, setFieldValue }) => (
          <form onSubmit={handleSubmit}>
            <Stack spacing={4} as={Container} maxW={"3xl"}>
              <Flex>
                <FormControl mr={2} isInvalid={!!errors.issueDate && touched.issueDate}>
                  <FormLabel fontSize={"sm"} htmlFor="issueDate">
                    Issue Date
                  </FormLabel>
                  <InputGroup size={Common.DEFAULT_FONT_SIZE}>
                    {/* <InputLeftAddon children="Issue Date" /> */}
                    <Field as={Input} id="issueDate" name="issueDate" type="datetime-local" />
                  </InputGroup>
                  <FormErrorMessage>{errors.issueDate}</FormErrorMessage>
                </FormControl>
                <FormControl mr={2} isInvalid={!!errors.statusId && touched.statusId}>
                  <FormLabel fontSize={"sm"} htmlFor="statusId">
                    Status
                  </FormLabel>
                  <Field as={Input} id="statusId" name="statusId" type="hidden" />
                  <FormErrorMessage>{errors.statusId}</FormErrorMessage>
                  <InvoiceStatusDropdown
                    selectedInvoiceStatus={invoiceStatus}
                    handleChange={(newValue?: InvoiceStatusRes) => {
                      setFieldValue("statusId", newValue?.invoiceStatusId ?? "");
                      setInvoiceStatus(newValue);
                      // console.log(newValue);
                    }}
                  ></InvoiceStatusDropdown>
                </FormControl>
              </Flex>
              <FormControl isInvalid={!!errors.userId && touched.userId}>
                <FormLabel fontSize={"sm"} htmlFor="userId">
                  Member
                </FormLabel>
                <Field as={Input} id="userId" name="userId" type="hidden" />
                <Field as={Input} id="email" name="email" type="hidden" />
                <FormErrorMessage>{errors.userId}</FormErrorMessage>
                <MemberDropdown
                  selectedMember={user}
                  handleChange={(newValue?: UserRes) => {
                    setFieldValue("userId", newValue?.id ?? "");
                    setFieldValue("email", newValue?.email ?? "");
                    setUser(newValue);
                    // console.log(newValue);
                  }}
                ></MemberDropdown>
              </FormControl>
              <FormControl isInvalid={!!errors.shippingAddressId && touched.shippingAddressId}>
                <FormLabel fontSize={"sm"} htmlFor="shippingAddressId">
                  Address
                </FormLabel>
                <Field as={Input} id="shippingAddressId" name="shippingAddressId" type="hidden" />
                <FormErrorMessage>{errors.shippingAddressId}</FormErrorMessage>
                <Select>
                  {addresses.map((item, index) => (
                    <option key={index} value={item.addressId}>
                      {item.address?.fullName} - {item.address?.address1}
                    </option>
                  ))}
                </Select>
              </FormControl>
              <Flex>
                <FormControl mr={2} isInvalid={!!errors.fullName && touched.fullName}>
                  <FormLabel fontSize={"sm"} htmlFor="fullName">
                    Full Name
                  </FormLabel>
                  <Field size={"sm"} as={Input} id="fullName" name="fullName" type="text" />
                  <FormErrorMessage>{errors.fullName}</FormErrorMessage>
                </FormControl>
                <FormControl isInvalid={!!errors.phone && touched.phone}>
                  <FormLabel fontSize={"sm"} htmlFor="phone">
                    Phone
                  </FormLabel>
                  <Field size={"sm"} as={Input} id="phone" name="phone" type="text" />
                  <FormErrorMessage>{errors.phone}</FormErrorMessage>
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
              <Flex>
                <FormControl mr={2} isInvalid={!!errors.city && touched.city}>
                  <FormLabel fontSize={"sm"} htmlFor="city">
                    City
                  </FormLabel>
                  <Field size={"sm"} as={Input} id="city" name="city" type="text" />
                  <FormErrorMessage>{errors.city}</FormErrorMessage>
                </FormControl>
                <FormControl isInvalid={!!errors.stateId && touched.stateId}>
                  <FormLabel fontSize={"sm"} htmlFor="stateId">
                    State
                  </FormLabel>
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
              </Flex>
              <Stack direction={"row"} spacing={6}>
                <Button size={"sm"} type="submit" colorScheme={"blue"}>
                  {updateText}
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
        <Heading fontSize={"lg"}>{updateText + " - " + (invoice?.fullName ?? "")}</Heading>
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
    <Box width={"3xl"} p={4}>
      <Stack spacing={4} as={Container} maxW={"3xl"}>
        {displayHeading()}
        {showUpdateForm()}
      </Stack>
    </Box>
  );
};

export default InvoiceEdit;
