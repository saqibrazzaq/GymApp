import { Select } from "chakra-react-select";
import React, { useEffect, useState } from "react";
import { PaymentMethodRes, PaymentMethodSearchReq } from "../../dtos/Payment";
import { PaymentMethodApi } from "../../api";

interface PaymentMethodDropdownParams {
  handleChange?: any;
  selectedPaymentMethod?: PaymentMethodRes;
}

const PaymentMethodDropdown = ({
  handleChange,
  selectedPaymentMethod,
}: PaymentMethodDropdownParams) => {
  const [inputValue, setInputValue] = useState("");
  const [items, setItems] = useState<PaymentMethodRes[]>([]);
  const [isLoading, setIsLoading] = useState(false);

  const loadPaymentMethods = () => {
    setIsLoading(true);
    PaymentMethodApi.search(new PaymentMethodSearchReq({ searchText: inputValue }, {}))
      .then((res) => {
        // console.log(res.pagedList);
        setItems(res.pagedList);
      })
      .finally(() => setIsLoading(false));
  };

  useEffect(() => {
    const timer = setTimeout(() => {
      loadPaymentMethods();
    }, 1000);

    return () => clearTimeout(timer);
  }, [inputValue]);

  const handleInputChange = (newValue: string) => {
    setInputValue(newValue);
  };

  return (
    <Select
      getOptionLabel={(c) => c.name || ""}
      getOptionValue={(c) => c.paymentMethodId || ""}
      options={items}
      onChange={handleChange}
      onInputChange={handleInputChange}
      isClearable={true}
      placeholder="Select payment method..."
      isLoading={isLoading}
      value={selectedPaymentMethod}
      size={"sm"}
    ></Select>
  );
};

export default PaymentMethodDropdown;
