import React, { useState } from "react";
import PropTypes from "prop-types";
import { TableCell } from "@material-ui/core";
import { TableRow } from "@material-ui/core";
import { Collapse } from "@material-ui/core";
import { IconButton } from "@material-ui/core";
import KeyboardArrowDownIcon from "@material-ui/icons/KeyboardArrowDown";
import KeyboardArrowUpIcon from "@material-ui/icons/KeyboardArrowUp";

export function CollapseRow(props) {
  const { children, row } = props;
  const [open, setOpen] = useState(false);

  return (
    <>
      <TableRow hover>
        <TableCell key={"collapse"}>
          <IconButton aria-label="expand row" size="small" onClick={() => setOpen(!open)}>
            {open ? <KeyboardArrowUpIcon /> : <KeyboardArrowDownIcon />}
          </IconButton>
        </TableCell>
        {row.map((cell) => (
          <TableCell key={cell.id} align={cell.align}>
            {cell.value}
          </TableCell>
        ))}
      </TableRow>
      <TableRow>
        <TableCell style={{ paddingBottom: 0, paddingTop: 0 }} colSpan={9}>
          <Collapse in={open} timeout="auto" unmountOnExit>
            {children}
          </Collapse>
        </TableCell>
      </TableRow>
    </>
  );
}

CollapseRow.propTypes = {
  children: PropTypes.node,
  row: PropTypes.array,
};
