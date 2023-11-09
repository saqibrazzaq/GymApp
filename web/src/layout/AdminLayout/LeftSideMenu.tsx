import {
  Accordion,
  AccordionButton,
  AccordionIcon,
  AccordionItem,
  AccordionPanel,
  Box,
  Container,
  Flex,
  FlexProps,
  Heading,
  Icon,
  Link,
  Stack,
  useColorModeValue,
} from "@chakra-ui/react";
import React from "react";
import { IconType } from "react-icons";
import { FiCompass, FiHome, FiSettings, FiStar, FiTrendingUp } from "react-icons/fi";
import { Link as RouteLink } from "react-router-dom";

export interface LinkItemProps {
  name: string;
  icon: IconType;
  href: string;
  children?: Array<LinkItemProps>;
}

interface SideMenuProps {
  menuItems: Array<LinkItemProps>;
}

const LeftSideMenu: React.FC<SideMenuProps> = ({ menuItems }: SideMenuProps) => {
  return (
    <Box minH="50vh">
      <Stack spacing={0} as={Container} maxW={"3xl"} textAlign={"left"} p={0}>
        {/* <Heading fontSize={"xl"}>{menuHeading}</Heading> */}
        <Accordion allowMultiple p={0} m={0}>
          {menuItems.map((menuItem) => (
            <NavItem
              name={menuItem.name}
              key={menuItem.name}
              icon={menuItem.icon}
              href={menuItem.href}
              children={menuItem.children}
            />
          ))}
        </Accordion>
      </Stack>
    </Box>
  );
};

interface NavItemProps {
  //  extends FlexProps
  icon: IconType;
  href: string;
  name: string;
  children?: LinkItemProps[];
}
const NavItem = ({ name, icon, children, href, ...rest }: NavItemProps) => {
  return (
    <AccordionItem>
      <h2>
        <AccordionButton>
          <Box as="span" flex="1" textAlign="left">
            {name}
          </Box>
          <AccordionIcon />
        </AccordionButton>
      </h2>
      <AccordionPanel pb={0}>
        {children?.map((item, index) => (
          <NavLink
            key={index}
            icon={item.icon}
            href={item.href}
            children={item.children}
            name={item.name}
          />
        ))}
      </AccordionPanel>
    </AccordionItem>
  );
};
const NavLink = ({ name, icon, children, href, ...rest }: NavItemProps) => {
  return (
    <Link
      as={RouteLink}
      to={href}
      style={{ textDecoration: "none" }}
      _focus={{ boxShadow: "none" }}
    >
      <Flex
        align="center"
        py="1"
        mx="0"
        borderRadius="lg"
        role="group"
        cursor="pointer"
        _hover={{
          bg: "#f5f5f5",
          color: "dark",
        }}
        {...rest}
      >
        {icon && (
          <Icon
            mr="2"
            fontSize="16"
            _groupHover={{
              color: "dark",
            }}
            as={icon}
          />
        )}
        {name}
      </Flex>
    </Link>
  );
};

export default LeftSideMenu;
