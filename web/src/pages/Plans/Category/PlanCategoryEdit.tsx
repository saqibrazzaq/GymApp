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
import { PlanCategoryEditReq } from "../../../dtos/Plan";
import { PlanCategoryApi } from "../../../api";
import { ErrorDetails } from "../../../dtos/Error";

const PlanCategoryEdit = () => {
  const params = useParams();
  const planCategoryId = params.planCategoryId;
  const updateText = planCategoryId ? "Update Plan Category" : "Create Plan Category";
  const [planCategory, setPlanCategory] = useState<PlanCategoryEditReq>(new PlanCategoryEditReq());
  const navigate = useNavigate();

  useEffect(() => {
    loadPlanCategory();
  }, []);

  const loadPlanCategory = () => {
    if (!planCategoryId) return;
    PlanCategoryApi.get(planCategoryId)
      .then((res) => {
        // console.log("country: " + res);
        setPlanCategory(res);
      })
      .catch((err) => {
        let errDetails: ErrorDetails = err?.response?.data;
        toastNotify(errDetails?.Message ?? "Error", "error");
      });
  };

  // Formik validation schema
  const validationSchema = Yup.object({
    name: Yup.string().required(),
  });

  const submitForm = (values: PlanCategoryEditReq) => {
    // console.log(values);
    if (planCategoryId) {
      updatePlanCategory(values);
    } else {
      createPlanCategory(values);
    }
  };

  const updatePlanCategory = (values: PlanCategoryEditReq) => {
    PlanCategoryApi.update(planCategoryId, values)
      .then((res) => {
        toastNotify("Plan Category updated successfully");
        navigate(-1);
      })
      .catch((err) => {
        let errDetails: ErrorDetails = err?.response?.data;
        toastNotify(errDetails?.Message ?? "Error", "error");
      });
  };

  const createPlanCategory = (values: PlanCategoryEditReq) => {
    PlanCategoryApi.create(values)
      .then((res) => {
        toastNotify("Plan Category created successfully");
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
        initialValues={planCategory}
        onSubmit={(values) => {
          submitForm(values);
        }}
        validationSchema={validationSchema}
        enableReinitialize={true}
      >
        {({ handleSubmit, errors, touched, setFieldValue }) => (
          <form onSubmit={handleSubmit}>
            <Stack spacing={4} as={Container} maxW={"3xl"}>
              <FormControl isInvalid={!!errors.name && touched.name}>
                <FormLabel fontSize={"sm"} htmlFor="name">
                  Name
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
        <Heading fontSize={"lg"}>{updateText + " - " + planCategory?.name}</Heading>
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

export default PlanCategoryEdit;
