import { Select } from "chakra-react-select";
import React, { useEffect, useState } from "react";
import { StaffApi } from "../../api/StaffApi";
import { RoleRes } from "../../dtos/User";

interface RoleDropdownParams {
  handleChange?: any;
  selectedRole?: RoleRes;
}

const RoleDropdown = ({ handleChange, selectedRole }: RoleDropdownParams) => {
  const [inputValue, setInputValue] = useState("");
  const [items, setItems] = useState<RoleRes[]>([]);
  const [isLoading, setIsLoading] = useState(false);

  const loadRoles = () => {
    setIsLoading(true);
    StaffApi.getAllRoles()
      .then((res) => {
        setItems(res);
      })
      .finally(() => setIsLoading(false));
  };

  useEffect(() => {
    const timer = setTimeout(() => {
      loadRoles();
    }, 1000);

    return () => clearTimeout(timer);
  }, [inputValue]);

  const handleInputChange = (newValue: string) => {
    setInputValue(newValue);
  };

  return (
    <Select
      getOptionLabel={(c) => c.roleName || ""}
      getOptionValue={(c) => c.roleName || ""}
      options={items}
      onChange={handleChange}
      onInputChange={handleInputChange}
      isClearable={true}
      placeholder="Select role..."
      isLoading={isLoading}
      value={selectedRole}
    ></Select>
  );
};

export default RoleDropdown;
