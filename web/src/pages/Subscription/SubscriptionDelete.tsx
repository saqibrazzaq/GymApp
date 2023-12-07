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
import { toastNotify } from "../../Helper";
import { ErrorDetails } from "../../dtos/Error";
import { SubscriptionRes } from "../../dtos/Subscription";
import { SubscriptionApi } from "../../api";

const SubscriptionDelete = () => {
  let params = useParams();
  const subscriptionId = params.subscriptionId;
  const [subscription, setSubscription] = useState<SubscriptionRes>();
  const navigate = useNavigate();
  const toast = useToast();

  const { isOpen, onOpen, onClose } = useDisclosure();
  const cancelRef = React.useRef<HTMLAnchorElement>(null);

  useEffect(() => {
    loadSubscription();
  }, [subscriptionId]);

  const loadSubscription = () => {
    if (!subscriptionId) return;
    SubscriptionApi.get(subscriptionId)
      .then((res) => {
        setSubscription(res);
        // console.log(res);
      })
      .catch((err) => {
        let errDetails: ErrorDetails = err?.response?.data;
        toastNotify(errDetails?.Message ?? "Error", "error");
      });
  };

  const deleteSubscription = () => {
    SubscriptionApi.delete(subscriptionId)
      .then((res) => {
        toastNotify(subscription?.plan?.name + " deleted successfully.");
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
        <Heading fontSize={"xl"}>Delete Subscription</Heading>
      </Box>
      <Spacer />
      <Box>
        <Button type="button" colorScheme={"gray"} onClick={() => navigate(-1)}>
          Back
        </Button>
      </Box>
    </Flex>
  );

  const showSubscriptionInfo = () => (
    <div>
      <Text fontSize="xl">Are you sure you want to delete the following Subscription?</Text>
      <TableContainer>
        <Table variant="simple">
          <Tbody>
            <Tr>
              <Th>Subscription Plan</Th>
              <Td>{subscription?.plan?.name}</Td>
            </Tr>
            <Tr>
              <Th>Member</Th>
              <Td>
                {subscription?.user?.fullName} - {subscription?.user?.email}
              </Td>
            </Tr>
          </Tbody>
        </Table>
      </TableContainer>
      <HStack pt={4} spacing={4}>
        <Button onClick={onOpen} type="button" colorScheme={"red"}>
          YES, I WANT TO DELETE THIS SUBSCRIPTION
        </Button>
      </HStack>
    </div>
  );

  const showAlertDialog = () => (
    <AlertDialog isOpen={isOpen} leastDestructiveRef={cancelRef} onClose={onClose}>
      <AlertDialogOverlay>
        <AlertDialogContent>
          <AlertDialogHeader fontSize="lg" fontWeight="bold">
            Delete Subscription
          </AlertDialogHeader>

          <AlertDialogBody>Are you sure? You can't undo this action afterwards.</AlertDialogBody>

          <AlertDialogFooter>
            <Link ref={cancelRef} onClick={onClose}>
              <Button type="button" colorScheme={"gray"}>
                Cancel
              </Button>
            </Link>
            <Link onClick={deleteSubscription} ml={3}>
              <Button type="submit" colorScheme={"red"}>
                Delete Subscription
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
        {showSubscriptionInfo()}
        {showAlertDialog()}
      </Stack>
    </Box>
  );
};

export default SubscriptionDelete;
