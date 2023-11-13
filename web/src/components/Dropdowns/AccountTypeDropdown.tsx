import { Select } from "chakra-react-select";
import React, { useEffect, useState } from "react";
import { AccountTypeRes, AccountTypeSearchReq } from "../../models/Account";
import { AccountTypeApi } from "../../api/AccountTypeApi";

interface AccountTypeDropdownParams {
  handleChange?: any;
  selectedAccountType?: AccountTypeRes;
}

const AccountTypeDropdown = ({ handleChange, selectedAccountType }: AccountTypeDropdownParams) => {
  const [inputValue, setInputValue] = useState("");
  const [items, setItems] = useState<AccountTypeRes[]>([]);
  const [isLoading, setIsLoading] = useState(false);

  const loadAccountTypes = () => {
    setIsLoading(true);
    AccountTypeApi.search(new AccountTypeSearchReq({ searchText: inputValue }, {}))
      .then((res) => {
        // console.log(res.pagedList);
        setItems(res.pagedList);
      })
      .finally(() => setIsLoading(false));
  };

  useEffect(() => {
    const timer = setTimeout(() => {
      loadAccountTypes();
    }, 1000);

    return () => clearTimeout(timer);
  }, [inputValue]);

  const handleInputChange = (newValue: string) => {
    setInputValue(newValue);
  };

  return (
    <Select
      getOptionLabel={(c) => c.name || ""}
      getOptionValue={(c) => c.accountTypeId || ""}
      options={items}
      onChange={handleChange}
      onInputChange={handleInputChange}
      isClearable={true}
      placeholder="Select account type..."
      isLoading={isLoading}
      value={selectedAccountType}
      size={"sm"}
    ></Select>
  );
};

export default AccountTypeDropdown;
