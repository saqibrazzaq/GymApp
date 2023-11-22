import {
  Box,
  Button,
  Center,
  Container,
  Flex,
  Heading,
  IconButton,
  Img,
  Input,
  Link,
  Spacer,
  Stack,
  Table,
  TableCaption,
  TableContainer,
  Tbody,
  Td,
  Text,
  Tfoot,
  Th,
  Thead,
  Tr,
} from "@chakra-ui/react";
import React, { useEffect, useState } from "react";
import { Link as RouteLink, useLocation, useNavigate, useSearchParams } from "react-router-dom";
import { PagedResponse } from "../../../dtos/Request";
import { Common } from "../../../utility";
import { toastNotify } from "../../../Helper";
import { BackButton, RegularButton } from "../../../components/Buttons";
import { DeleteIconButton, EditIconButton } from "../../../components/Icons";
import { CurrencyRes } from "../../../dtos/Currency";
import { CurrencyApi } from "../../../api";
import { ErrorDetails } from "../../../dtos/Error";

const Currencies = () => {
  const location = useLocation();
  const [searchParams, setSearchParams] = useSearchParams(location.search);
  searchParams.set("pageSize", Common.DEFAULT_PAGE_SIZE.toString());
  const [searchText, setSearchText] = useState<string>(searchParams.get("searchText") ?? "");
  const [pagedRes, setPagedRes] = useState<PagedResponse<CurrencyRes>>();
  const navigate = useNavigate();

  useEffect(() => {
    searchCurrencies();
  }, [searchParams]);

  const updateSearchParams = (key: string, value: string) => {
    searchParams.set(key, value);
    setSearchParams(searchParams);
  };

  const previousPage = () => {
    if (pagedRes?.metaData) {
      let previousPageNumber = (pagedRes?.metaData?.currentPage || 2) - 1;
      updateSearchParams("pageNumber", previousPageNumber.toString());
    }
  };

  const nextPage = () => {
    if (pagedRes?.metaData) {
      let nextPageNumber = (pagedRes?.metaData?.currentPage || 0) + 1;
      updateSearchParams("pageNumber", nextPageNumber.toString());
    }
  };

  const searchCurrencies = () => {
    CurrencyApi.search(Object.fromEntries(searchParams))
      .then((res) => {
        setPagedRes(res);
      })
      .catch((err) => {
        let errDetails: ErrorDetails = err?.response?.data;
        toastNotify(errDetails?.Message ?? "Error", "error");
      });
  };

  const displayHeading = () => (
    <Flex>
      <Box>
        <Heading fontSize={"xl"}>Search Currencies</Heading>
      </Box>
      <Spacer />
      <Box>
        <Link as={RouteLink} to={"edit"}>
          <RegularButton text="Create Currency" />
        </Link>
        <Link ml={2} onClick={() => navigate(-1)}>
          <BackButton />
        </Link>
      </Box>
    </Flex>
  );

  const displayCurrencies = () => (
    <TableContainer>
      <Table variant="simple" size={"sm"}>
        <Thead>
          <Tr>
            <Th>Code</Th>
            <Th>Name</Th>
            <Th></Th>
          </Tr>
        </Thead>
        <Tbody>
          {pagedRes?.pagedList ? (
            pagedRes.pagedList.map((item) => (
              <Tr key={item.currencyId}>
                <Td>{item.code}</Td>
                <Td>{item.name}</Td>
                <Td>
                  <Link as={RouteLink} ms={2} to={item.currencyId + "/edit"}>
                    <EditIconButton />
                  </Link>
                  <Link as={RouteLink} ms={2} to={item.currencyId + "/delete"}>
                    <DeleteIconButton />
                  </Link>
                </Td>
              </Tr>
            ))
          ) : (
            <></>
          )}
        </Tbody>
        <Tfoot>
          <Tr>
            <Th colSpan={3} textAlign="center">
              <Button
                isDisabled={!pagedRes?.metaData?.hasPrevious}
                variant="link"
                mr={5}
                onClick={previousPage}
              >
                Previous
              </Button>
              Page {pagedRes?.metaData?.currentPage} of {pagedRes?.metaData?.totalPages}
              <Button
                isDisabled={!pagedRes?.metaData?.hasNext}
                variant="link"
                ml={5}
                onClick={nextPage}
              >
                Next
              </Button>
            </Th>
          </Tr>
        </Tfoot>
      </Table>
    </TableContainer>
  );

  const displaySearchBar = () => (
    <Flex>
      <Center></Center>
      <Box flex={1} ml={4}></Box>

      <Box ml={4}>
        <Input
          size={"sm"}
          placeholder="Search..."
          value={searchText}
          onChange={(e) => setSearchText(e.currentTarget.value)}
          onKeyDown={(e) => {
            if (e.key === "Enter") {
              updateSearchParams("searchText", searchText);
              updateSearchParams("pageNumber", "1");
            }
          }}
        />
      </Box>
      <Box ml={0}>
        <Button
          colorScheme={"blue"}
          size={"sm"}
          onClick={() => {
            updateSearchParams("searchText", searchText);
            updateSearchParams("pageNumber", "1");
          }}
        >
          Search
        </Button>
      </Box>
    </Flex>
  );

  return (
    <Box p={4}>
      <Stack spacing={4} as={Container} maxW={"3xl"}>
        {displayHeading()}
        {displaySearchBar()}
        {displayCurrencies()}
      </Stack>
    </Box>
  );
};

export default Currencies;
