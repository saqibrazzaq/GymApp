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
import { CurrencyRes } from "../../../dtos/Currency";
import { CurrencyApi } from "../../../api";
import { ErrorDetails } from "../../../dtos/Error";

const CurrencyDelete = () => {
  let params = useParams();
  const { currencyId } = params;
  const [currency, setCurrency] = useState<CurrencyRes>();
  const navigate = useNavigate();
  const [error, setError] = useState("");

  const { isOpen, onOpen, onClose } = useDisclosure();
  const cancelRef = React.useRef<HTMLAnchorElement>(null);

  useEffect(() => {
    loadCurrency();
  }, [currencyId]);

  const loadCurrency = () => {
    if (!currencyId) return;
    CurrencyApi.get(currencyId)
      .then((res) => {
        setCurrency(res);
        // console.log(res);
      })
      .catch((err) => {
        let errDetails: ErrorDetails = err?.response?.data;
        toastNotify(errDetails?.Message ?? "Error", "error");
      });
  };

  const deleteCurrency = () => {
    setError("");
    CurrencyApi.delete(currencyId)
      .then((res) => {
        toastNotify(currency?.name + " deleted successfully.");
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
        <Heading fontSize={"xl"}>Delete Currency</Heading>
      </Box>
      <Spacer />
      <Box>
        <Button type="button" colorScheme={"gray"} onClick={() => navigate(-1)}>
          Back
        </Button>
      </Box>
    </Flex>
  );

  const showCurrencyInfo = () => (
    <div>
      <Text fontSize="xl">Are you sure you want to delete the following Currency?</Text>
      <TableContainer>
        <Table variant="simple">
          <Tbody>
            <Tr>
              <Th>Currency Name</Th>
              <Td>{currency?.name}</Td>
            </Tr>
            <Tr>
              <Th>Currency Code</Th>
              <Td>{currency?.code}</Td>
            </Tr>
          </Tbody>
        </Table>
      </TableContainer>
      <HStack pt={4} spacing={4}>
        <Button onClick={onOpen} type="button" colorScheme={"red"}>
          YES, I WANT TO DELETE THIS CURRENCY
        </Button>
      </HStack>
    </div>
  );

  const showAlertDialog = () => (
    <AlertDialog isOpen={isOpen} leastDestructiveRef={cancelRef} onClose={onClose}>
      <AlertDialogOverlay>
        <AlertDialogContent>
          <AlertDialogHeader fontSize="lg" fontWeight="bold">
            Delete Currency
          </AlertDialogHeader>

          <AlertDialogBody>Are you sure? You can't undo this action afterwards.</AlertDialogBody>

          <AlertDialogFooter>
            <Link ref={cancelRef} onClick={onClose}>
              <Button type="button" colorScheme={"gray"}>
                Cancel
              </Button>
            </Link>
            <Link onClick={deleteCurrency} ml={3}>
              <Button type="submit" colorScheme={"red"}>
                Delete Currency Name
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
        {showCurrencyInfo()}
        {showAlertDialog()}
      </Stack>
    </Box>
  );
};

export default CurrencyDelete;
