import React from "react";
import { toolbarStyles } from "./tableStyles";
import { green } from "@material-ui/core/colors";
import PropTypes from "prop-types";

import IconButton from "@material-ui/core/IconButton";
import AddIconCircle from "@material-ui/icons/AddCircleRounded";
import Toolbar from "@material-ui/core/Toolbar";
import Tooltip from "@material-ui/core/Tooltip";
import FilterListIcon from "@material-ui/icons/FilterList";

export function CustomTableToolbar(props) {
  const classes = toolbarStyles();

  return (
    <Toolbar>
      <div className={classes.title}>
        <Tooltip title="Dodaj jednostkę">
          <IconButton aria-label="add survey" onClick={props.onAdd}>
            <AddIconCircle style={{ color: green[700] }} />
          </IconButton>
        </Tooltip>
      </div>
      <div>
        <Tooltip title="Lista filtrów">
          <IconButton aria-label="filter list">
            <FilterListIcon />
          </IconButton>
        </Tooltip>
      </div>
    </Toolbar>
  );
}

CustomTableToolbar.propTypes = {
  onAdd: PropTypes.func,
};
