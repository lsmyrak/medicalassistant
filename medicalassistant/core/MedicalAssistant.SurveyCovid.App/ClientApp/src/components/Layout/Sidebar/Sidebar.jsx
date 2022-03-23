import React from "react";
import PropTypes from "prop-types";

import { Role } from "../../../shared/constants";
import { useAuthService } from "../../../shared/hooks";
import { useHistory } from "react-router-dom";

import clsx from "clsx";
import { makeStyles, useTheme } from "@material-ui/core/styles";
import Drawer from "@material-ui/core/Drawer";
import List from "@material-ui/core/List";
import Divider from "@material-ui/core/Divider";
import IconButton from "@material-ui/core/IconButton";
import ChevronLeftIcon from "@material-ui/icons/ChevronLeft";
import ChevronRightIcon from "@material-ui/icons/ChevronRight";
import ListItem from "@material-ui/core/ListItem";
import ListItemIcon from "@material-ui/core/ListItemIcon";
import ListItemText from "@material-ui/core/ListItemText";
import ViewListIcon from "@material-ui/icons/ViewList";
import PagesIcon from "@material-ui/icons/Pages";
import HelpIcon from "@material-ui/icons/Help";
import SettingsIcon from "@material-ui/icons/Settings";
import LocalHospitalIcon from "@material-ui/icons/LocalHospital";
import LibraryBooksIcon from "@material-ui/icons/LibraryBooks";
import ReportIcon from "@material-ui/icons/Report";

const useStyles = makeStyles((theme) => ({
  root: {
    display: "flex",
  },
  drawer: {
    width: (props) => props.drawerWidth,
    flexShrink: 0,
    whiteSpace: "nowrap",
  },
  drawerOpen: {
    width: (props) => props.drawerWidth,
    transition: theme.transitions.create("width", {
      easing: theme.transitions.easing.sharp,
      duration: theme.transitions.duration.enteringScreen,
    }),
  },
  drawerClose: {
    transition: theme.transitions.create("width", {
      easing: theme.transitions.easing.sharp,
      duration: theme.transitions.duration.leavingScreen,
    }),
    overflowX: "hidden",
    width: theme.spacing(7) + 1,
    [theme.breakpoints.up("sm")]: {
      width: theme.spacing(7) + 1,
    },
  },
  toolbar: {
    display: "flex",
    alignItems: "center",
    justifyContent: "flex-end",
    padding: theme.spacing(0, 1),
    // necessary for content to be below app bar
    ...theme.mixins.toolbar,
  },
}));

const userMenu = [
  {
    key: 1,
    nameMenu: "Strona główna",
    icon: <PagesIcon />,
    roles: [Role.ADMIN, Role.USER],
    url: "/",
  },
  {
    key: 2,
    nameMenu: "Lista ankiet",
    icon: <ViewListIcon />,
    roles: [Role.ADMIN, Role.USER],
    url: "/survey",
  },
];

const adminMenu = [
  {
    key: 1,
    nameMenu: "Pomoc",
    icon: <HelpIcon />,
    roles: [Role.ADMIN, Role.USER],
    url: "/",
  },
  {
    key: 2,
    nameMenu: "Konfiguracja",
    icon: <SettingsIcon />,
    roles: [Role.ADMIN],
    url: "/",
  },
  {
    key: 3,
    nameMenu: "Miejsca",
    icon: <LocalHospitalIcon />,
    roles: [Role.ADMIN],
    url: "/department",
  },
  {
    key: 4,
    nameMenu: "Produkty",
    icon: <LibraryBooksIcon />,
    roles: [Role.ADMIN],
    url: "/product",
  },
  {
    key: 5,
    nameMenu: "Raporty",
    icon: <ReportIcon />,
    roles: [Role.ADMIN],
    url: "/reports",
  },
];

export function Sidebar(props) {
  const classes = useStyles({ drawerWidth: props.drawerWidth });
  const theme = useTheme();
  const authService = useAuthService();
  const history = useHistory();

  const getMenuItem = (menuItems) =>
    menuItems
      .filter((item) => authService.hasRequiredRole(item.roles))
      .map((item) => (
        <ListItem button key={item.key} onClick={() => history.push(item.url)}>
          <ListItemIcon>{item.icon}</ListItemIcon>
          <ListItemText primary={item.nameMenu} />
        </ListItem>
      ));

  return (
    <>
      <Drawer
        variant="permanent"
        className={clsx({
          [classes.drawerOpen]: props.isOpen,
          [classes.drawerClose]: !props.isOpen,
        })}
        classes={{
          paper: clsx({
            [classes.drawerOpen]: props.isOpen,
            [classes.drawerClose]: !props.isOpen,
          }),
        }}
      >
        <div className={classes.toolbar}>
          <ListItem>
            <ListItemIcon></ListItemIcon>
            <ListItemText primary={"MENU"} />
          </ListItem>
          <IconButton onClick={props.handleDrawerClose}>
            {theme.direction === "rtl" ? <ChevronRightIcon /> : <ChevronLeftIcon />}
          </IconButton>
        </div>
        <Divider />
        <List>{getMenuItem(userMenu)}</List>
        <Divider />
        <List>{getMenuItem(adminMenu)}</List>
      </Drawer>
    </>
  );
}

Sidebar.propTypes = {
  handleDrawerClose: PropTypes.func,
  isOpen: PropTypes.bool,
  drawerWidth: PropTypes.number,
};
