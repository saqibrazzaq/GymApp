import {
  AlertDialog,
  AlertDialogBody,
  AlertDialogContent,
  AlertDialogFooter,
  AlertDialogHeader,
  AlertDialogOverlay,
  Box,
  Button,
  Container,
  Flex,
  Heading,
  HStack,
  Link,
  Spacer,
  Stack,
  Table,
  TableContainer,
  Tbody,
  Td,
  Text,
  Th,
  Tr,
  useDisclosure,
  useToast,
} from "@chakra-ui/react";
import React, { useEffect, useState } from "react";
import { useParams, Link as RouteLink, useNavigate } from "react-router-dom";
import { toastNotify } from "../../../Helper";
import { ErrorDetails } from "../../../models/Error";
import { PlanRes } from "../../../models/Plan";
import { PlanApi } from "../../../api";

const PlanDelete = () => {
  let params = useParams();
  const { planId } = params;
  const [plan, setPlan] = useState<PlanRes>();
  const navigate = useNavigate();
  const toast = useToast();
  const [error, setError] = useState("");

  const { isOpen, onOpen, onClose } = useDisclosure();
  const cancelRef = React.useRef<HTMLAnchorElement>(null);

  useEffect(() => {
    loadPlan();
  }, [planId]);

  const loadPlan = () => {
    if (!planId) return;
    PlanApi.get(planId)
      .then((res) => {
        setPlan(res);
        // console.log(res);
      })
      .catch((err) => {
        let errDetails: ErrorDetails = err?.response?.data;
        toastNotify(errDetails?.Message ?? "Error", "error");
      });
  };

  const deletePlan = () => {
    setError("");
    PlanApi.delete(planId)
      .then((res) => {
        toastNotify(plan?.name + " deleted successfully.");
        navigate(-1);
      })
      .catch((err) => {
        let errDetails: ErrorDetails = err?.response?.data;
        toastNotify(errDetails?.Message ?? "Error", "error");
      });
  };

  const displayHeading = () => (
    <Flex>
      <Box>
        <Heading fontSize={"xl"}>Delete Plan</Heading>
      </Box>
      <Spacer />
      <Box>
        <Button type="button" colorScheme={"gray"} onClick={() => navigate(-1)}>
          Back
        </Button>
      </Box>
    </Flex>
  );

  const showPlanInfo = () => (
    <div>
      <Text fontSize="xl">Are you sure you want to delete the following Plan?</Text>
      <TableContainer>
        <Table variant="simple">
          <Tbody>
            <Tr>
              <Th>Name</Th>
              <Td>{plan?.name}</Td>
            </Tr>
            <Tr>
              <Th>Plan Category</Th>
              <Td>{plan?.planCategory?.name}</Td>
            </Tr>
            <Tr>
              <Th>Plan Type</Th>
              <Td>{plan?.planType?.name}</Td>
            </Tr>
          </Tbody>
        </Table>
      </TableContainer>
      <HStack pt={4} spacing={4}>
        <Button onClick={onOpen} type="button" colorScheme={"red"}>
          YES, I WANT TO DELETE THIS PLAN
        </Button>
      </HStack>
    </div>
  );

  const showAlertDialog = () => (
    <AlertDialog isOpen={isOpen} leastDestructiveRef={cancelRef} onClose={onClose}>
      <AlertDialogOverlay>
        <AlertDialogContent>
          <AlertDialogHeader fontSize="lg" fontWeight="bold">
            Delete Plan
          </AlertDialogHeader>

          <AlertDialogBody>Are you sure? You can't undo this action afterwards.</AlertDialogBody>

          <AlertDialogFooter>
            <Link ref={cancelRef} onClick={onClose}>
              <Button type="button" colorScheme={"gray"}>
                Cancel
              </Button>
            </Link>
            <Link onClick={deletePlan} ml={3}>
              <Button type="submit" colorScheme={"red"}>
                Delete Plan
              </Button>
            </Link>
          </AlertDialogFooter>
        </AlertDialogContent>
      </AlertDialogOverlay>
    </AlertDialog>
  );

  return (
    <Box p={4}>
      <Stack spacing={4} as={Container} maxW={"3xl"}>
        {displayHeading()}
        {showPlanInfo()}
        {showAlertDialog()}
      </Stack>
    </Box>
  );
};

export default PlanDelete;
