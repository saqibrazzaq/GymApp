import {
  Alert,
  AlertDescription,
  AlertDialog,
  AlertDialogBody,
  AlertDialogContent,
  AlertDialogFooter,
  AlertDialogHeader,
  AlertDialogOverlay,
  AlertIcon,
  AlertTitle,
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
import { Link as RouteLink, useNavigate, useParams } from "react-router-dom";
import { ErrorAlert } from "../../dtos/Error/AlertBoxes";
import { UserRes } from "../../dtos/User";
import ErrorDetails from "../../dtos/Error/ErrorDetails";
import { toastNotify } from "../../Helper";
import { CancelButton, DeleteButton, BackButton } from "../../components/Buttons";
import { MemberApi } from "../../api";

const MemberDelete = () => {
  const { isOpen, onOpen, onClose } = useDisclosure();
  const cancelRef = React.useRef<HTMLAnchorElement>(null);

  const [user, setUser] = useState<UserRes>();
  const [error, setError] = useState("");

  const toast = useToast();

  const navigate = useNavigate();
  let params = useParams();
  const username = params.username || "";

  useEffect(() => {
    MemberApi.getUserByName(username)
      .then((res) => {
        // console.log(res);
        setUser(res);
        setError("");
      })
      .catch((err) => {
        let errDetails: ErrorDetails = err?.response?.data;
        setError(errDetails?.Message || "Service failed");
        toastNotify(errDetails?.Message || "Service failed", "error");
      });
  }, []);

  const deleteMember = () => {
    onClose();
    MemberApi.deleteMember(username)
      .then((res) => {
        // console.log("User deleted successfully.");
        toastNotify(username + " deleted successfully.");
        navigate(-1);
      })
      .catch((err) => {
        console.log(err);
        let errDetails: ErrorDetails = err?.response?.data;
        setError(errDetails?.Message || "Service failed");
        toastNotify(errDetails?.Message || "Service failed", "error");
      });
  };

  const showMemberInfo = () => (
    <div>
      <TableContainer>
        <Table variant="simple">
          <Tbody>
            <Tr>
              <Th>Username</Th>
              <Td>{user?.fullName}</Td>
            </Tr>
            <Tr>
              <Th>Email</Th>
              <Td>{user?.email}</Td>
            </Tr>
          </Tbody>
        </Table>
      </TableContainer>
      <HStack pt={4} spacing={4}>
        <Link onClick={onOpen}>
          <DeleteButton text="YES, I WANT TO DELETE THIS MEMBER" />
        </Link>
      </HStack>
    </div>
  );

  const showAlertDialog = () => (
    <AlertDialog isOpen={isOpen} leastDestructiveRef={cancelRef} onClose={onClose}>
      <AlertDialogOverlay>
        <AlertDialogContent>
          <AlertDialogHeader fontSize="lg" fontWeight="bold">
            Delete Member
          </AlertDialogHeader>

          <AlertDialogBody>Are you sure? You can't undo this action afterwards.</AlertDialogBody>

          <AlertDialogFooter>
            <Link ref={cancelRef} onClick={onClose}>
              <CancelButton />
            </Link>
            <Link onClick={deleteMember} ml={3}>
              <DeleteButton text="Delete Member" />
            </Link>
          </AlertDialogFooter>
        </AlertDialogContent>
      </AlertDialogOverlay>
    </AlertDialog>
  );

  const displayHeading = () => (
    <Flex>
      <Box>
        <Heading fontSize={"xl"}>Delete Member</Heading>
      </Box>
      <Spacer />
      <Box>
        <Link ml={2} onClick={() => navigate(-1)}>
          <BackButton />
        </Link>
      </Box>
    </Flex>
  );

  return (
    <Box p={4}>
      <Stack spacing={4} as={Container} maxW={"3xl"}>
        {displayHeading()}
        <Text fontSize="xl">Are you sure you want to delete the following member?</Text>
        {error && <ErrorAlert description={error} />}
        {showMemberInfo()}
      </Stack>
      {showAlertDialog()}
    </Box>
  );
};

export default MemberDelete;
