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
import { SubscriptionEditReq } from "../../dtos/Subscription";
import { PlanRes } from "../../dtos/Plan";
import { UserRes } from "../../dtos/User";
import { SubscriptionApi } from "../../api";
import { Common } from "../../utility";
import PlanDropdown from "../../components/Dropdowns/PlanDropdown";
import { MemberDropdown } from "../../components/Dropdowns";

const SubscriptionEdit = () => {
  const params = useParams();
  const subscriptionId = params.subscriptionId;
  const updateText = subscriptionId ? "Update Subscription" : "Create Subscription";
  const [subscription, setSubscription] = useState<SubscriptionEditReq>(new SubscriptionEditReq());
  const [plan, setPlan] = useState<PlanRes>();
  const [user, setUser] = useState<UserRes>();
  const navigate = useNavigate();

  useEffect(() => {
    loadSubscription();
  }, []);

  const loadSubscription = () => {
    if (!subscriptionId) return;
    SubscriptionApi.get(subscriptionId)
      .then((res) => {
        // console.log(res);
        setSubscription(res);
        setPlan(res.plan);
        setUser(res.user);
      })
      .catch((err) => {
        let errDetails: ErrorDetails = err?.response?.data;
        toastNotify(errDetails?.Message ?? "Error", "error");
      });
  };

  // Formik validation schema
  const validationSchema = Yup.object({
    activeFrom: Yup.date(),
    activeTo: Yup.date(),
    planId: Yup.string().required(),
    userId: Yup.string().required(),
  });

  const submitForm = (values: SubscriptionEditReq) => {
    values = convertEmptyStringToNull(values);
    if (subscriptionId) {
      updateSubscription(values);
    } else {
      createSubscription(values);
    }
  };

  const convertEmptyStringToNull = (obj: SubscriptionEditReq) => {
    obj.planId = obj.planId == "" ? undefined : obj.planId;
    obj.userId = obj.userId == "" ? undefined : obj.userId;
    return obj;
  };

  const updateSubscription = (values: SubscriptionEditReq) => {
    SubscriptionApi.update(subscriptionId, values)
      .then((res) => {
        toastNotify("Subscription updated successfully");
        navigate(-1);
      })
      .catch((err) => {
        let errDetails: ErrorDetails = err?.response?.data;
        toastNotify(errDetails?.Message ?? "Error", "error");
      });
  };

  const createSubscription = (values: SubscriptionEditReq) => {
    SubscriptionApi.create(values)
      .then((res) => {
        toastNotify("Subscription created successfully");
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
        initialValues={subscription}
        onSubmit={(values) => {
          submitForm(values);
        }}
        validationSchema={validationSchema}
        enableReinitialize={true}
      >
        {({ handleSubmit, errors, touched, setFieldValue }) => (
          <form onSubmit={handleSubmit}>
            <Stack spacing={4} as={Container} maxW={"3xl"}>
              <FormControl isInvalid={!!errors.activeFrom && touched.activeFrom}>
                <InputGroup size={Common.DEFAULT_FONT_SIZE}>
                  <InputLeftAddon children="Active From" />
                  <Field as={Input} id="activeFrom" name="activeFrom" type="datetime-local" />
                </InputGroup>
                <FormErrorMessage>{errors.activeFrom}</FormErrorMessage>
              </FormControl>
              <FormControl isInvalid={!!errors.activeTo && touched.activeTo}>
                <InputGroup size={Common.DEFAULT_FONT_SIZE}>
                  <InputLeftAddon children="Active To" />
                  <Field as={Input} id="activeTo" name="activeTo" type="datetime-local" />
                </InputGroup>
                <FormErrorMessage>{errors.activeTo}</FormErrorMessage>
              </FormControl>
              <FormControl mr={2} isInvalid={!!errors.planId && touched.planId}>
                <FormLabel fontSize={"sm"} htmlFor="planId">
                  Plan
                </FormLabel>
                <Field as={Input} id="planId" name="planId" type="hidden" />
                <FormErrorMessage>{errors.planId}</FormErrorMessage>
                <PlanDropdown
                  selectedPlan={plan}
                  handleChange={(newValue?: PlanRes) => {
                    setFieldValue("planId", newValue?.planId ?? "");
                    setPlan(newValue);
                    // console.log(newValue);
                  }}
                ></PlanDropdown>
              </FormControl>
              <FormControl isInvalid={!!errors.userId && touched.userId}>
                <FormLabel fontSize={"sm"} htmlFor="userId">
                  Member
                </FormLabel>
                <Field as={Input} id="userId" name="userId" type="hidden" />
                <FormErrorMessage>{errors.userId}</FormErrorMessage>
                <MemberDropdown
                  selectedMember={user}
                  handleChange={(newValue?: UserRes) => {
                    setFieldValue("userId", newValue?.id ?? "");
                    setUser(newValue);
                    // console.log(newValue);
                  }}
                ></MemberDropdown>
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
        <Heading fontSize={"lg"}>{updateText + " - " + subscription?.user?.fullName}</Heading>
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

export default SubscriptionEdit;
