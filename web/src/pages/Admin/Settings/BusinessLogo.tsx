import {
  Alert,
  AlertDescription,
  AlertIcon,
  AlertTitle,
  Box,
  Container,
  FormControl,
  FormErrorMessage,
  FormLabel,
  Heading,
  Image,
  Input,
  Stack,
  useToast,
} from "@chakra-ui/react";
import React, { useCallback, useEffect, useState } from "react";
import { useDropzone } from "react-dropzone";
import { useNavigate } from "react-router-dom";
import { useDispatch } from "react-redux";
import { Common } from "../../../utility";
import { AccountApi } from "../../../api/AccountApi";
import { ErrorAlert, ErrorDetails } from "../../../dtos/Error";
import { toastNotify } from "../../../Helper";
import { SubmitButton } from "../../../components/Buttons";

const BusinessLogo = () => {
  const [error, setError] = useState("");
  const [image, setImage] = useState(Common.DEFAULT_PROFILE_PICTURE);
  const toast = useToast();
  const navigate = useNavigate();
  const dispatch = useDispatch();

  useEffect(() => {
    loadAccountInfo();
  }, [image]);

  const loadAccountInfo = () => {
    setError("");
    AccountApi.get()
      .then((res) => {
        // console.log("Load user info");
        // console.log(res);
        if (res.logoUrl) {
          // console.log("Profile picture set");
          setImage(res.logoUrl ?? "");
        }
      })
      .catch((err) => {
        console.log(err);
        let errDetails: ErrorDetails = err?.response?.data;
        setError(errDetails?.Message || "Service failed.");
      });
  };

  const showImage = () => <Image boxSize="200px" src={image} />;

  const handleSubmit = (event: any) => {
    event.preventDefault();
    AccountApi.updateLogo(fd)
      .then((res) => {
        // console.log(res.data);
        toastNotify("Business Logo updated successfully");
        //loadAccountInfo();
        acceptedFiles.splice(0);
        navigate(-1);
      })
      .catch((err) => {
        console.log(err);
      });
  };

  // const config = { headers: { "Content-Type": "multipart/form-data" } };
  let fd = new FormData();

  const { acceptedFiles, getRootProps, getInputProps } = useDropzone();

  const files = acceptedFiles.map((file) => (
    <li key={file.name}>
      {file.name} - {file.size} bytes
    </li>
  ));

  acceptedFiles.map((file) => {
    fd.append("File[]", file);
  });

  const showForm = () => (
    <form method="post" onSubmit={handleSubmit} encType="multipart/form-data">
      <FormControl>
        <div {...getRootProps({ className: "dropzone" })}>
          <input {...getInputProps()} />
          <p>Drag 'n' drop some files here, or click to select files</p>
        </div>
        <aside>
          <h4>Files</h4>
          <ul>{files}</ul>
        </aside>
      </FormControl>
      <Stack spacing={6}>
        <SubmitButton text="Upload Business Logo" />
      </Stack>
    </form>
  );

  return (
    <Box p={4}>
      <Stack spacing={4} as={Container} maxW={"3xl"}>
        <Heading fontSize={"xl"}>Business Logo</Heading>
        {error && <ErrorAlert description={error} />}
        {image && showImage()}
        {showForm()}
      </Stack>
    </Box>
  );
};

export default BusinessLogo;
