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
  Link,
  Spacer,
  Stack,
  useToast,
} from "@chakra-ui/react";
import { useState, useEffect } from "react";
import { Link as RouteLink, useNavigate, useParams } from "react-router-dom";
import * as Yup from "yup";
import { Field, Formik } from "formik";
import { toastNotify } from "../../../Helper";
import { CurrencyEditReq } from "../../../dtos/Currency";
import { CurrencyApi } from "../../../api";
import { ErrorDetails } from "../../../dtos/Error";

const CurrencyEdit = () => {
  const params = useParams();
  const currencyId = params.currencyId;
  const updateText = currencyId ? "Update Currency" : "Create Currency";
  const [currency, setCurrency] = useState<CurrencyEditReq>(new CurrencyEditReq());
  const navigate = useNavigate();

  useEffect(() => {
    loadCurrency();
  }, []);

  const loadCurrency = () => {
    if (!currencyId) return;
    CurrencyApi.get(currencyId)
      .then((res) => {
        // console.log("country: " + res);
        setCurrency(res);
      })
      .catch((err) => {
        let errDetails: ErrorDetails = err?.response?.data;
        toastNotify(errDetails?.Message ?? "Error", "error");
      });
  };

  // Formik validation schema
  const validationSchema = Yup.object({
    name: Yup.string().required(),
    code: Yup.string().required().max(3),
  });

  const submitForm = (values: CurrencyEditReq) => {
    // console.log(values);
    if (currencyId) {
      updateCurrency(values);
    } else {
      createCurrency(values);
    }
  };

  const updateCurrency = (values: CurrencyEditReq) => {
    CurrencyApi.update(currencyId, values)
      .then((res) => {
        toastNotify("Currency updated successfully");
        navigate(-1);
      })
      .catch((err) => {
        let errDetails: ErrorDetails = err?.response?.data;
        toastNotify(errDetails?.Message ?? "Error", "error");
      });
  };

  const createCurrency = (values: CurrencyEditReq) => {
    CurrencyApi.create(values)
      .then((res) => {
        toastNotify("Currency created successfully");
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
        initialValues={currency}
        onSubmit={(values) => {
          submitForm(values);
        }}
        validationSchema={validationSchema}
        enableReinitialize={true}
      >
        {({ handleSubmit, errors, touched, setFieldValue }) => (
          <form onSubmit={handleSubmit}>
            <Stack spacing={4} as={Container} maxW={"3xl"}>
              <FormControl isInvalid={!!errors.code && touched.code}>
                <FormLabel fontSize={"sm"} htmlFor="code">
                  Currency Code
                </FormLabel>
                <Field size={"sm"} as={Input} id="code" name="code" type="text" />
                <FormErrorMessage>{errors.code}</FormErrorMessage>
              </FormControl>
              <FormControl isInvalid={!!errors.name && touched.name}>
                <FormLabel fontSize={"sm"} htmlFor="name">
                  Currency Name
                </FormLabel>
                <Field size={"sm"} as={Input} id="name" name="name" type="text" />
                <FormErrorMessage>{errors.name}</FormErrorMessage>
              </FormControl>
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
        <Heading fontSize={"lg"}>{updateText + " - " + currency?.name}</Heading>
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

export default CurrencyEdit;
