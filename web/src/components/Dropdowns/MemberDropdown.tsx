import { Select } from "chakra-react-select";
import React, { useEffect, useState } from "react";
import { SearchUsersReq, UserRes } from "../../dtos/User";
import { MemberApi } from "../../api";

interface MemberDropdownParams {
  handleChange?: any;
  selectedMember?: UserRes;
}

const MemberDropdown = ({ handleChange, selectedMember }: MemberDropdownParams) => {
  const [inputValue, setInputValue] = useState("");
  const [items, setItems] = useState<UserRes[]>([]);
  const [isLoading, setIsLoading] = useState(false);

  const loadMembers = () => {
    setIsLoading(true);
    MemberApi.search(new SearchUsersReq({ searchText: inputValue }, {}))
      .then((res) => {
        // console.log(res.pagedList);
        setItems(res.pagedList);
      })
      .finally(() => setIsLoading(false));
  };

  useEffect(() => {
    const timer = setTimeout(() => {
      loadMembers();
    }, 1000);

    return () => clearTimeout(timer);
  }, [inputValue]);

  const handleInputChange = (newValue: string) => {
    setInputValue(newValue);
  };

  return (
    <Select
      getOptionLabel={(c) => c.fullName + " - " + c.email || ""}
      getOptionValue={(c) => c.id || ""}
      options={items}
      onChange={handleChange}
      onInputChange={handleInputChange}
      isClearable={true}
      placeholder="Select member..."
      isLoading={isLoading}
      value={selectedMember}
      size={"sm"}
    ></Select>
  );
};

export default MemberDropdown;
