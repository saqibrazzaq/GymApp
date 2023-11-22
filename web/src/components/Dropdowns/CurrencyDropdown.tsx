import { Select } from "chakra-react-select";
import React, { useEffect, useState } from "react";
import { CurrencyRes, CurrencySearchReq } from "../../dtos/Currency";
import { CurrencyApi } from "../../api";

interface CurrencyDropdownParams {
  handleChange?: any;
  selectedCurrency?: CurrencyRes;
}

const CurrencyDropdown = ({ handleChange, selectedCurrency }: CurrencyDropdownParams) => {
  const [inputValue, setInputValue] = useState("");
  const [items, setItems] = useState<CurrencyRes[]>([]);
  const [isLoading, setIsLoading] = useState(false);

  const loadCurrencies = () => {
    setIsLoading(true);
    CurrencyApi.search(new CurrencySearchReq({ searchText: inputValue }, {}))
      .then((res) => {
        // console.log(res.pagedList);
        setItems(res.pagedList);
      })
      .finally(() => setIsLoading(false));
  };

  useEffect(() => {
    const timer = setTimeout(() => {
      loadCurrencies();
    }, 1000);

    return () => clearTimeout(timer);
  }, [inputValue]);

  const handleInputChange = (newValue: string) => {
    setInputValue(newValue);
  };

  return (
    <Select
      getOptionLabel={(c) => c.code + " - " + c.name || ""}
      getOptionValue={(c) => c.currencyId || ""}
      options={items}
      onChange={handleChange}
      onInputChange={handleInputChange}
      isClearable={true}
      placeholder="Select currency..."
      isLoading={isLoading}
      value={selectedCurrency}
      size={"sm"}
    ></Select>
  );
};

export default CurrencyDropdown;
