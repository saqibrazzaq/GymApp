import { Select } from "chakra-react-select";
import React, { useEffect, useState } from "react";
import { InvoiceStatusRes, InvoiceStatusSearchReq } from "../../dtos/Invoice";
import { InvoiceStatusApi } from "../../api";

interface InvoiceStatusDropdownParams {
  handleChange?: any;
  selectedInvoiceStatus?: InvoiceStatusRes;
}

const InvoiceStatusDropdown = ({
  handleChange,
  selectedInvoiceStatus,
}: InvoiceStatusDropdownParams) => {
  const [inputValue, setInputValue] = useState("");
  const [items, setItems] = useState<InvoiceStatusRes[]>([]);
  const [isLoading, setIsLoading] = useState(false);

  const loadInvoiceStatus = () => {
    setIsLoading(true);
    InvoiceStatusApi.search(new InvoiceStatusSearchReq({ searchText: inputValue }, {}))
      .then((res) => {
        // console.log(res.pagedList);
        setItems(res.pagedList);
      })
      .finally(() => setIsLoading(false));
  };

  useEffect(() => {
    const timer = setTimeout(() => {
      loadInvoiceStatus();
    }, 1000);

    return () => clearTimeout(timer);
  }, [inputValue]);

  const handleInputChange = (newValue: string) => {
    setInputValue(newValue);
  };

  return (
    <Select
      getOptionLabel={(c) => c.name || ""}
      getOptionValue={(c) => c.invoiceStatusId || ""}
      options={items}
      onChange={handleChange}
      onInputChange={handleInputChange}
      isClearable={true}
      placeholder="Select invoice status..."
      isLoading={isLoading}
      value={selectedInvoiceStatus}
      size={"sm"}
    ></Select>
  );
};

export default InvoiceStatusDropdown;
