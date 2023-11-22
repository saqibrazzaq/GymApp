import { Select } from "chakra-react-select";
import React, { useEffect, useState } from "react";
import { LeadStatusRes, LeadStatusSearchReq } from "../../dtos/User";
import { LeadStatusApi } from "../../api";

interface LeadStatusDropdownParams {
  handleChange?: any;
  selectedLeadStatus?: LeadStatusRes;
}

const LeadStatusDropdown = ({ handleChange, selectedLeadStatus }: LeadStatusDropdownParams) => {
  const [inputValue, setInputValue] = useState("");
  const [items, setItems] = useState<LeadStatusRes[]>([]);
  const [isLoading, setIsLoading] = useState(false);

  const loadLeadStatus = () => {
    setIsLoading(true);
    LeadStatusApi.search(new LeadStatusSearchReq({ searchText: inputValue }, {}))
      .then((res) => {
        // console.log(res.pagedList);
        setItems(res.pagedList);
      })
      .finally(() => setIsLoading(false));
  };

  useEffect(() => {
    const timer = setTimeout(() => {
      loadLeadStatus();
    }, 1000);

    return () => clearTimeout(timer);
  }, [inputValue]);

  const handleInputChange = (newValue: string) => {
    setInputValue(newValue);
  };

  return (
    <Select
      getOptionLabel={(c) => c.name || ""}
      getOptionValue={(c) => c.leadStatusId || ""}
      options={items}
      onChange={handleChange}
      onInputChange={handleInputChange}
      isClearable={true}
      placeholder="Select lead status..."
      isLoading={isLoading}
      value={selectedLeadStatus}
      size={"sm"}
    ></Select>
  );
};

export default LeadStatusDropdown;
