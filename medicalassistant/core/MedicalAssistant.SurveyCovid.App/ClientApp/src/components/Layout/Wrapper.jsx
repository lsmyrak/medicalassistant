import React from "react";
import PropTypes from "prop-types";

import { TopBar } from "./TopBar/TopBar";
import { makeStyles } from "@material-ui/core/styles";
import CssBaseline from "@material-ui/core/CssBaseline";
import { Sidebar } from "./Sidebar/Sidebar";

const drawerWidth = 240;

const useStyles = makeStyles((theme) => ({
  root: {
    display: "flex",
  },
  toolbar: {
    display: "flex",
    alignItems: "center",
    justifyContent: "flex-end",
    padding: theme.spacing(0, 1),
    // necessary for content to be below app bar
    ...theme.mixins.toolbar,
  },
  content: {
    flexGrow: 1,
    padding: theme.spacing(5),
  },
}));

export function Wrapper(props) {
  const classes = useStyles();
  const [isOpen, setIsOpen] = React.useState(false);

  const handleDrawerOpen = () => {
    setIsOpen(true);
  };

  const handleDrawerClose = () => {
    setIsOpen(false);
  };

  return (
    <div className={classes.root}>
      {/* <CssBaseline /> */}
      <TopBar handleDrawerOpen={handleDrawerOpen} isOpen={isOpen} drawerWidth={drawerWidth}></TopBar>
      <Sidebar handleDrawerClose={handleDrawerClose} isOpen={isOpen} drawerWidth={drawerWidth}></Sidebar>
      <main className={classes.content}>
        <div className={classes.toolbar} />
        {props.children}
      </main>
    </div>
  );
}

// export function Wrapper(props) {
//   return (
//     <div>
//       <div>
//         <NavMenu />
//         <div className="container-fluid" style={{ height: "820px" }}>
//           {props.children}
//         </div>
//         <Footer />
//       </div>
//     </div>
//   );
// }

Wrapper.propTypes = {
  children: PropTypes.node,
};
