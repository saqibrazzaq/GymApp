import { Select } from "chakra-react-select";
import React, { useEffect, useState } from "react";
import { PlanCategoryRes, PlanCategorySearchReq } from "../../dtos/Plan";
import { PlanCategoryApi } from "../../api";

interface PlanCategoryDropdownParams {
  handleChange?: any;
  selectedPlanCategory?: PlanCategoryRes;
}

const PlanCategoryDropdown = ({
  handleChange,
  selectedPlanCategory,
}: PlanCategoryDropdownParams) => {
  const [inputValue, setInputValue] = useState("");
  const [items, setItems] = useState<PlanCategoryRes[]>([]);
  const [isLoading, setIsLoading] = useState(false);

  const loadPlanCategories = () => {
    setIsLoading(true);
    PlanCategoryApi.search(new PlanCategorySearchReq({ searchText: inputValue }, {}))
      .then((res) => {
        // console.log(res.pagedList);
        setItems(res.pagedList);
      })
      .finally(() => setIsLoading(false));
  };

  useEffect(() => {
    const timer = setTimeout(() => {
      loadPlanCategories();
    }, 1000);

    return () => clearTimeout(timer);
  }, [inputValue]);

  const handleInputChange = (newValue: string) => {
    setInputValue(newValue);
  };

  return (
    <Select
      getOptionLabel={(c) => c.name || ""}
      getOptionValue={(c) => c.planCategoryId || ""}
      options={items}
      onChange={handleChange}
      onInputChange={handleInputChange}
      isClearable={true}
      placeholder="Select plan category..."
      isLoading={isLoading}
      value={selectedPlanCategory}
      size={"sm"}
    ></Select>
  );
};

export default PlanCategoryDropdown;
