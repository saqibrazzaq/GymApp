import { IconButton, Tooltip } from "@chakra-ui/react";
import { FaMapMarkedAlt } from "react-icons/fa";
import IconButtonProps from "./IconButtonProps";

const StateIconButton = ({ size = "xs", fontSize = "16" }: IconButtonProps) => {
  return (
    <Tooltip label="States">
      <IconButton
        variant="outline"
        size={size}
        fontSize={fontSize}
        colorScheme="blue"
        icon={<FaMapMarkedAlt />}
        aria-label="States"
      />
    </Tooltip>
  );
};

export default StateIconButton;
