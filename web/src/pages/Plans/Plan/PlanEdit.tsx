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
import { ErrorDetails } from "../../../dtos/Error";
import { PlanCategoryRes, PlanEditReq, PlanTypeRes, TimeUnitRes } from "../../../dtos/Plan";
import { PlanApi } from "../../../api";
import {
  PlanCategoryDropdown,
  PlanTypeDropdown,
  TimeUnitDropdown,
} from "../../../components/Dropdowns";

const PlanEdit = () => {
  const params = useParams();
  const planId = params.planId;
  const updateText = planId ? "Update Plan" : "Create Plan";
  const [plan, setPlan] = useState<PlanEditReq>(new PlanEditReq());
  const [planCategory, setPlanCategory] = useState<PlanCategoryRes>();
  const [planType, setPlanType] = useState<PlanTypeRes>();
  const [timeUnit, setTimeUnit] = useState<TimeUnitRes>();
  const navigate = useNavigate();

  useEffect(() => {
    loadPlan();
  }, []);

  const loadPlan = () => {
    if (!planId) return;
    PlanApi.get(planId)
      .then((res) => {
        // console.log("country: " + res);
        setPlan(res);
        setPlanCategory(res.planCategory);
        setPlanType(res.planType);
        setTimeUnit(res.timeUnit);
      })
      .catch((err) => {
        let errDetails: ErrorDetails = err?.response?.data;
        toastNotify(errDetails?.Message ?? "Error", "error");
      });
  };

  // Formik validation schema
  const validationSchema = Yup.object({
    name: Yup.string().required(),
    description: Yup.string(),
    planCategoryId: Yup.string(),
    planTypeId: Yup.string(),
    duration: Yup.number(),
    timeUnitId: Yup.string(),
    setupFee: Yup.number(),
    price: Yup.number(),
  });

  const submitForm = (values: PlanEditReq) => {
    values = convertEmptyStringToNull(values);
    if (planId) {
      updatePlan(values);
    } else {
      createPlan(values);
    }
  };

  const convertNullToEmptyString = (obj: PlanEditReq) => {
    obj.planCategoryId ??= "";
    obj.planTypeId ??= "";
    obj.timeUnitId ??= "";
    return obj;
  };

  const convertEmptyStringToNull = (obj: PlanEditReq) => {
    obj.planCategoryId = obj.planCategoryId == "" ? undefined : obj.planCategoryId;
    obj.planTypeId = obj.planTypeId == "" ? undefined : obj.planTypeId;
    obj.timeUnitId = obj.timeUnitId == "" ? undefined : obj.timeUnitId;
    return obj;
  };

  const updatePlan = (values: PlanEditReq) => {
    PlanApi.update(planId, values)
      .then((res) => {
        toastNotify("Plan updated successfully");
        navigate(-1);
      })
      .catch((err) => {
        let errDetails: ErrorDetails = err?.response?.data;
        toastNotify(errDetails?.Message ?? "Error", "error");
      });
  };

  const createPlan = (values: PlanEditReq) => {
    PlanApi.create(values)
      .then((res) => {
        toastNotify("Plan created successfully");
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
        initialValues={convertNullToEmptyString(plan)}
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
              <FormControl isInvalid={!!errors.description && touched.description}>
                <FormLabel fontSize={"sm"} htmlFor="description">
                  Description
                </FormLabel>
                <Field size={"sm"} as={Input} id="description" name="description" type="text" />
                <FormErrorMessage>{errors.description}</FormErrorMessage>
              </FormControl>
              <Flex>
                <FormControl mr={2} isInvalid={!!errors.planCategoryId && touched.planCategoryId}>
                  <FormLabel fontSize={"sm"} htmlFor="planCategoryId">
                    Plan Category
                  </FormLabel>
                  <Field as={Input} id="planCategoryId" name="planCategoryId" type="hidden" />
                  <FormErrorMessage>{errors.planCategoryId}</FormErrorMessage>
                  <PlanCategoryDropdown
                    selectedPlanCategory={planCategory}
                    handleChange={(newValue?: PlanCategoryRes) => {
                      setFieldValue("planCategoryId", newValue?.planCategoryId ?? "");
                      setPlanCategory(newValue);
                      // console.log(newValue);
                    }}
                  ></PlanCategoryDropdown>
                </FormControl>
                <FormControl isInvalid={!!errors.planTypeId && touched.planTypeId}>
                  <FormLabel fontSize={"sm"} htmlFor="planTypeId">
                    Plan Type
                  </FormLabel>
                  <Field as={Input} id="planTypeId" name="planTypeId" type="hidden" />
                  <FormErrorMessage>{errors.planTypeId}</FormErrorMessage>
                  <PlanTypeDropdown
                    selectedPlanType={planType}
                    handleChange={(newValue?: PlanTypeRes) => {
                      setFieldValue("planTypeId", newValue?.planTypeId ?? "");
                      setPlanType(newValue);
                      // console.log(newValue);
                    }}
                  ></PlanTypeDropdown>
                </FormControl>
              </Flex>
              <Flex>
                <FormControl mr={2} isInvalid={!!errors.duration && touched.duration}>
                  <FormLabel fontSize={"sm"} htmlFor="duration">
                    Duration
                  </FormLabel>
                  <Field size={"sm"} as={Input} id="duration" name="duration" type="text" />
                  <FormErrorMessage>{errors.duration}</FormErrorMessage>
                </FormControl>
                <FormControl isInvalid={!!errors.timeUnitId && touched.timeUnitId}>
                  <FormLabel fontSize={"sm"} htmlFor="timeUnitId">
                    Time Unit
                  </FormLabel>
                  <Field as={Input} id="timeUnitId" name="timeUnitId" type="hidden" />
                  <FormErrorMessage>{errors.timeUnitId}</FormErrorMessage>
                  <TimeUnitDropdown
                    selectedTimeUnit={timeUnit}
                    handleChange={(newValue?: TimeUnitRes) => {
                      setFieldValue("timeUnitId", newValue?.timeUnitId ?? "");
                      setTimeUnit(newValue);
                      // console.log(newValue);
                    }}
                  ></TimeUnitDropdown>
                </FormControl>
              </Flex>
              <Flex>
                <FormControl mr={2} isInvalid={!!errors.setupFee && touched.setupFee}>
                  <FormLabel fontSize={"sm"} htmlFor="setupFee">
                    Setup Fee
                  </FormLabel>
                  <Field size={"sm"} as={Input} id="setupFee" name="setupFee" type="text" />
                  <FormErrorMessage>{errors.setupFee}</FormErrorMessage>
                </FormControl>
                <FormControl isInvalid={!!errors.price && touched.price}>
                  <FormLabel fontSize={"sm"} htmlFor="price">
                    Price
                  </FormLabel>
                  <Field size={"sm"} as={Input} id="price" name="price" type="text" />
                  <FormErrorMessage>{errors.price}</FormErrorMessage>
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
        <Heading fontSize={"lg"}>{updateText + " - " + plan?.name}</Heading>
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
    <Box width={"xl"} p={4}>
      <Stack spacing={4} as={Container} maxW={"3xl"}>
        {displayHeading()}
        {showUpdateForm()}
      </Stack>
    </Box>
  );
};

export default PlanEdit;
