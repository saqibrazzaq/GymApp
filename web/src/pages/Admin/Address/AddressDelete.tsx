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
import { AddressRes } from "../../../models/Address";
import { UserAddressApi } from "../../../api";
import { ErrorDetails } from "../../../models/Error";

const AddressDelete = () => {
  let params = useParams();
  const userAddressId = params.userAddressId;
  const [address, setAddress] = useState<AddressRes>();
  const navigate = useNavigate();
  const toast = useToast();
  const [error, setError] = useState("");

  const { isOpen, onOpen, onClose } = useDisclosure();
  const cancelRef = React.useRef<HTMLAnchorElement>(null);

  useEffect(() => {
    loadUserAddress();
  }, [userAddressId]);

  const loadUserAddress = () => {
    if (!userAddressId) return;
    UserAddressApi.get(userAddressId)
      .then((res) => {
        console.log(res);
        setAddress(res.address);
      })
      .catch((err) => {
        let errDetails: ErrorDetails = err?.response?.data;
        toastNotify(errDetails?.Message ?? "Error", "error");
      });
  };

  const deleteUserAddress = () => {
    setError("");
    UserAddressApi.delete(userAddressId)
      .then((res) => {
        toastNotify(address?.address1 + " deleted successfully.");
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
        <Heading fontSize={"xl"}>Delete Address</Heading>
      </Box>
      <Spacer />
      <Box>
        <Button type="button" colorScheme={"gray"} onClick={() => navigate(-1)}>
          Back
        </Button>
      </Box>
    </Flex>
  );

  const showAddressInfo = () => (
    <div>
      <Text fontSize="xl">Are you sure you want to delete the following Address?</Text>
      <TableContainer>
        <Table variant="simple">
          <Tbody>
            <Tr>
              <Th>Address</Th>
              <Td>
                {address?.address1}
                <br />
                {address?.address2}
                <br />
                {address?.city}, {address?.state?.stateName}, {address?.state?.country?.countryName}
              </Td>
            </Tr>
          </Tbody>
        </Table>
      </TableContainer>
      <HStack pt={4} spacing={4}>
        <Button onClick={onOpen} type="button" colorScheme={"red"}>
          YES, I WANT TO DELETE THIS ADDRESS
        </Button>
      </HStack>
    </div>
  );

  const showAlertDialog = () => (
    <AlertDialog isOpen={isOpen} leastDestructiveRef={cancelRef} onClose={onClose}>
      <AlertDialogOverlay>
        <AlertDialogContent>
          <AlertDialogHeader fontSize="lg" fontWeight="bold">
            Delete Address
          </AlertDialogHeader>

          <AlertDialogBody>Are you sure? You can't undo this action afterwards.</AlertDialogBody>

          <AlertDialogFooter>
            <Link ref={cancelRef} onClick={onClose}>
              <Button type="button" colorScheme={"gray"}>
                Cancel
              </Button>
            </Link>
            <Link onClick={deleteUserAddress} ml={3}>
              <Button type="submit" colorScheme={"red"}>
                Delete Address
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
        {showAddressInfo()}
        {showAlertDialog()}
      </Stack>
    </Box>
  );
};

export default AddressDelete;
