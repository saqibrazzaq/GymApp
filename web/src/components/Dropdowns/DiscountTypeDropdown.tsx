import { Select } from "chakra-react-select";
import React, { useEffect, useState } from "react";
import { DiscountTypeRes, DiscountTypeSearchReq } from "../../dtos/Invoice";
import { DiscountTypeApi } from "../../api";

interface DiscountTypeDropdownParams {
  handleChange?: any;
  selectedDiscountType?: DiscountTypeRes;
}

const DiscountTypeDropdown = ({
  handleChange,
  selectedDiscountType,
}: DiscountTypeDropdownParams) => {
  const [inputValue, setInputValue] = useState("");
  const [items, setItems] = useState<DiscountTypeRes[]>([]);
  const [isLoading, setIsLoading] = useState(false);

  const loadDiscountTypes = () => {
    setIsLoading(true);
    DiscountTypeApi.search(new DiscountTypeSearchReq({ searchText: inputValue }, {}))
      .then((res) => {
        // console.log(res.pagedList);
        setItems(res.pagedList);
      })
      .finally(() => setIsLoading(false));
  };

  useEffect(() => {
    const timer = setTimeout(() => {
      loadDiscountTypes();
    }, 1000);

    return () => clearTimeout(timer);
  }, [inputValue]);

  const handleInputChange = (newValue: string) => {
    setInputValue(newValue);
  };

  return (
    <Select
      getOptionLabel={(c) => c.name || ""}
      getOptionValue={(c) => c.discountTypeId || ""}
      options={items}
      onChange={handleChange}
      onInputChange={handleInputChange}
      isClearable={true}
      placeholder="Select discount type..."
      isLoading={isLoading}
      value={selectedDiscountType}
      size={"sm"}
    ></Select>
  );
};

export default DiscountTypeDropdown;
