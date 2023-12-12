import { Select } from "chakra-react-select";
import React, { useEffect, useState } from "react";
import { TimeUnitRes, TimeUnitSearchReq } from "../../dtos/Plan";
import { TimeUnitApi } from "../../api";

interface TimeUnitDropdownParams {
  handleChange?: any;
  selectedTimeUnit?: TimeUnitRes;
}

const TimeUnitDropdown = ({ handleChange, selectedTimeUnit }: TimeUnitDropdownParams) => {
  const [inputValue, setInputValue] = useState("");
  const [items, setItems] = useState<TimeUnitRes[]>([]);
  const [isLoading, setIsLoading] = useState(false);

  const loadTimeUnits = () => {
    setIsLoading(true);
    TimeUnitApi.search(new TimeUnitSearchReq({ searchText: inputValue }, {}))
      .then((res) => {
        // console.log(res.pagedList);
        setItems(res.pagedList);
      })
      .finally(() => setIsLoading(false));
  };

  useEffect(() => {
    const timer = setTimeout(() => {
      loadTimeUnits();
    }, 1000);

    return () => clearTimeout(timer);
  }, [inputValue]);

  const handleInputChange = (newValue: string) => {
    setInputValue(newValue);
  };

  return (
    <Select
      getOptionLabel={(c) => c.name || ""}
      getOptionValue={(c) => c.timeUnitId || ""}
      options={items}
      onChange={handleChange}
      onInputChange={handleInputChange}
      isClearable={true}
      placeholder="Select time unit..."
      isLoading={isLoading}
      value={selectedTimeUnit}
      size={"sm"}
    ></Select>
  );
};

export default TimeUnitDropdown;
