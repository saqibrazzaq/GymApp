import { Select } from "chakra-react-select";
import React, { useEffect, useState } from "react";
import { PlanRes, PlanSearchReq } from "../../dtos/Plan";
import { PlanApi } from "../../api";

interface PlanDropdownParams {
  handleChange?: any;
  selectedPlan?: PlanRes;
}

const PlanDropdown = ({ handleChange, selectedPlan }: PlanDropdownParams) => {
  const [inputValue, setInputValue] = useState("");
  const [items, setItems] = useState<PlanRes[]>([]);
  const [isLoading, setIsLoading] = useState(false);

  const loadPlans = () => {
    setIsLoading(true);
    PlanApi.search(new PlanSearchReq({ searchText: inputValue }, {}))
      .then((res) => {
        // console.log(res.pagedList);
        setItems(res.pagedList);
      })
      .finally(() => setIsLoading(false));
  };

  useEffect(() => {
    const timer = setTimeout(() => {
      loadPlans();
    }, 1000);

    return () => clearTimeout(timer);
  }, [inputValue]);

  const handleInputChange = (newValue: string) => {
    setInputValue(newValue);
  };

  return (
    <Select
      getOptionLabel={(c) => c.name || ""}
      getOptionValue={(c) => c.planId || ""}
      options={items}
      onChange={handleChange}
      onInputChange={handleInputChange}
      isClearable={true}
      placeholder="Select plan..."
      isLoading={isLoading}
      value={selectedPlan}
      size={"sm"}
    ></Select>
  );
};

export default PlanDropdown;
