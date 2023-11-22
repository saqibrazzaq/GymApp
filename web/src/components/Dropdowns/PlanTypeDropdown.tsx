import { Select } from "chakra-react-select";
import React, { useEffect, useState } from "react";
import { PlanTypeRes, PlanTypeSearchReq } from "../../dtos/Plan";
import { PlanTypeApi } from "../../api/PlanTypeApi";

interface PlanTypeDropdownParams {
  handleChange?: any;
  selectedPlanType?: PlanTypeRes;
}

const PlanTypeDropdown = ({ handleChange, selectedPlanType }: PlanTypeDropdownParams) => {
  const [inputValue, setInputValue] = useState("");
  const [items, setItems] = useState<PlanTypeRes[]>([]);
  const [isLoading, setIsLoading] = useState(false);

  const loadPlanTypes = () => {
    setIsLoading(true);
    PlanTypeApi.search(new PlanTypeSearchReq({ searchText: inputValue }, {}))
      .then((res) => {
        // console.log(res.pagedList);
        setItems(res.pagedList);
      })
      .finally(() => setIsLoading(false));
  };

  useEffect(() => {
    const timer = setTimeout(() => {
      loadPlanTypes();
    }, 1000);

    return () => clearTimeout(timer);
  }, [inputValue]);

  const handleInputChange = (newValue: string) => {
    setInputValue(newValue);
  };

  return (
    <Select
      getOptionLabel={(c) => c.name || ""}
      getOptionValue={(c) => c.planTypeId || ""}
      options={items}
      onChange={handleChange}
      onInputChange={handleInputChange}
      isClearable={true}
      placeholder="Select plan type..."
      isLoading={isLoading}
      value={selectedPlanType}
      size={"sm"}
    ></Select>
  );
};

export default PlanTypeDropdown;
