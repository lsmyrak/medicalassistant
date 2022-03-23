import React from "react";
import PropTypes from "prop-types";
import { useHistory, useLocation } from "react-router-dom";

import { useAuthService } from "../../../shared/hooks";

import clsx from "clsx";
import { makeStyles } from "@material-ui/core/styles";
import AppBar from "@material-ui/core/AppBar";
import Toolbar from "@material-ui/core/Toolbar";
import Button from "@material-ui/core/Button";
import Typography from "@material-ui/core/Typography";
import Box from "@material-ui/core/Box";
import IconButton from "@material-ui/core/IconButton";
import MenuIcon from "@material-ui/icons/Menu";
import LockIcon from "@material-ui/icons/Lock";
//import HowToRegIcon from "@material-ui/icons/HowToReg";

const Role = {
  admin: "ADMINISTRATOR",
  user: "PERSONEL MEDYCZNY",
};

const useStyles = makeStyles((theme) => ({
  root: {
    flexGrow: 1,
  },
  appBar: {
    zIndex: theme.zIndex.drawer + 1,
    transition: theme.transitions.create(["width", "margin"], {
      easing: theme.transitions.easing.sharp,
      duration: theme.transitions.duration.leavingScreen,
    }),
  },
  appBarShift: (props) => ({
    marginLeft: props.drawerWidth,
    width: `calc(100% - ${props.drawerWidth}px)`,
    transition: theme.transitions.create(["width", "margin"], {
      easing: theme.transitions.easing.sharp,
      duration: theme.transitions.duration.enteringScreen,
    }),
  }),
  menuButton: {
    marginRight: 36,
  },
  hide: {
    display: "none",
  },
}));

export function TopBar(props) {
  const classes = useStyles({ drawerWidth: props.drawerWidth });
  const authService = useAuthService();
  const history = useHistory();
  const location = useLocation();

  const logout = () => {
    authService.logout();
    console.log(location.pathname);
    history.replace(location.pathname);
  };

  return (
    <header>
      <AppBar
        position="fixed"
        className={clsx(classes.appBar, {
          [classes.appBarShift]: props.isOpen,
        })}
      >
        <Toolbar>
          <IconButton
            color="inherit"
            aria-label="open drawer"
            onClick={props.handleDrawerOpen}
            edge="start"
            className={clsx(classes.menuButton, {
              [classes.hide]: props.isOpen,
            })}
          >
            <MenuIcon />
          </IconButton>
          <Box flexGrow={1}>
            <Typography>{`Zalogowano jako ${Role[authService.getUserProfile().role]}`}</Typography>
            {/* <HowToRegIcon /> */}
          </Box>
          <Button color="inherit" onClick={logout}>
            <Box mr={1}>
              <LockIcon />
            </Box>
            <Box>Wyloguj</Box>
          </Button>
        </Toolbar>
      </AppBar>
    </header>
  );
}

TopBar.propTypes = {
  handleDrawerOpen: PropTypes.func,
  isOpen: PropTypes.bool,
  drawerWidth: PropTypes.number,
};
