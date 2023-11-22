import { Select } from "chakra-react-select";
import React, { useEffect, useState } from "react";
import { UserTypeRes, UserTypeSearchReq } from "../../dtos/User";
import { UserTypeApi } from "../../api";

interface UserTypeDropdownParams {
  handleChange?: any;
  selectedUserType?: UserTypeRes;
}

const UserTypeDropdown = ({ handleChange, selectedUserType }: UserTypeDropdownParams) => {
  const [inputValue, setInputValue] = useState("");
  const [items, setItems] = useState<UserTypeRes[]>([]);
  const [isLoading, setIsLoading] = useState(false);

  const loadUserTypes = () => {
    setIsLoading(true);
    UserTypeApi.search(new UserTypeSearchReq({ searchText: inputValue }, {}))
      .then((res) => {
        // console.log(res.pagedList);
        setItems(res.pagedList);
      })
      .finally(() => setIsLoading(false));
  };

  useEffect(() => {
    const timer = setTimeout(() => {
      loadUserTypes();
    }, 1000);

    return () => clearTimeout(timer);
  }, [inputValue]);

  const handleInputChange = (newValue: string) => {
    setInputValue(newValue);
  };

  return (
    <Select
      getOptionLabel={(c) => c.name || ""}
      getOptionValue={(c) => c.userTypeId || ""}
      options={items}
      onChange={handleChange}
      onInputChange={handleInputChange}
      isClearable={true}
      placeholder="Select user type..."
      isLoading={isLoading}
      value={selectedUserType}
      size={"sm"}
    ></Select>
  );
};

export default UserTypeDropdown;
