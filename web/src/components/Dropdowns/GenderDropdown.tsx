import { Select } from "chakra-react-select";
import React, { useEffect, useState } from "react";
import { GenderRes, GenderSearchReq } from "../../dtos/User";
import { GenderApi } from "../../api";

interface GenderDropdownParams {
  handleChange?: any;
  selectedGender?: GenderRes;
}

const GenderDropdown = ({ handleChange, selectedGender }: GenderDropdownParams) => {
  const [inputValue, setInputValue] = useState("");
  const [items, setItems] = useState<GenderRes[]>([]);
  const [isLoading, setIsLoading] = useState(false);

  const loadGender = () => {
    setIsLoading(true);
    GenderApi.search(new GenderSearchReq({ searchText: inputValue }, {}))
      .then((res) => {
        // console.log(res.pagedList);
        setItems(res.pagedList);
      })
      .finally(() => setIsLoading(false));
  };

  useEffect(() => {
    const timer = setTimeout(() => {
      loadGender();
    }, 1000);

    return () => clearTimeout(timer);
  }, [inputValue]);

  const handleInputChange = (newValue: string) => {
    setInputValue(newValue);
  };

  return (
    <Select
      getOptionLabel={(c) => c.name || ""}
      getOptionValue={(c) => c.genderId || ""}
      options={items}
      onChange={handleChange}
      onInputChange={handleInputChange}
      isClearable={true}
      placeholder="Select gender..."
      isLoading={isLoading}
      value={selectedGender}
      size={"sm"}
    ></Select>
  );
};

export default GenderDropdown;
