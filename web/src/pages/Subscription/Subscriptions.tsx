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
import { PagedResponse } from "../../dtos/Request";
import { Common } from "../../utility";
import { toastNotify } from "../../Helper";
import { BackButton, RegularButton } from "../../components/Buttons";
import { DeleteIconButton, EditIconButton, StateIconButton } from "../../components/Icons";
import { ErrorDetails } from "../../dtos/Error";
import { SubscriptionRes } from "../../dtos/Subscription";
import { SubscriptionApi } from "../../api";

const Subscriptions = () => {
  const location = useLocation();
  const [searchParams, setSearchParams] = useSearchParams(location.search);
  searchParams.set("pageSize", Common.DEFAULT_PAGE_SIZE.toString());
  const [searchText, setSearchText] = useState<string>(searchParams.get("searchText") ?? "");
  const [pagedRes, setPagedRes] = useState<PagedResponse<SubscriptionRes>>();
  const navigate = useNavigate();

  useEffect(() => {
    searchSubscriptions();
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

  const searchSubscriptions = () => {
    SubscriptionApi.search(Object.fromEntries(searchParams))
      .then((res) => {
        //let userRes: PagedResponse<UserDto> = res;
        // console.log(res);
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
        <Heading fontSize={"xl"}>Subscriptions</Heading>
      </Box>
      <Spacer />
      <Box>
        <Link as={RouteLink} to={"edit"}>
          <RegularButton text="Create Subscriptions" />
        </Link>
        <Link ml={2} onClick={() => navigate(-1)}>
          <BackButton />
        </Link>
      </Box>
    </Flex>
  );

  const displaySubscriptions = () => (
    <TableContainer>
      <Table variant="simple" size={"sm"}>
        <Thead>
          <Tr>
            <Th>Name</Th>
            <Th>Plan</Th>
            <Th>Duration</Th>
            <Th>Status</Th>
            <Th></Th>
          </Tr>
        </Thead>
        <Tbody>
          {pagedRes?.pagedList ? (
            pagedRes.pagedList.map((item) => (
              <Tr key={item.subscriptionId}>
                <Td>{item.user?.fullName}</Td>
                <Td>{item.planName}</Td>
                <Td>
                  {Common.formatDate(item.activeFrom)} to <br /> {Common.formatDate(item.activeTo)}
                </Td>
                <Td>{item.status ? "Active" : "Inactive"}</Td>
                <Td>
                  <Link as={RouteLink} ms={2} to={item.subscriptionId + "/edit"}>
                    <EditIconButton />
                  </Link>
                  <Link as={RouteLink} ms={2} to={item.subscriptionId + "/delete"}>
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
        {displaySubscriptions()}
      </Stack>
    </Box>
  );
};

export default Subscriptions;
