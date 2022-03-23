import React from "react";
import PropTypes from "prop-types";
import { TableCell } from "@material-ui/core";
import { TableRow } from "@material-ui/core";

export function BasicRow(props) {
  return (
    <TableRow hover>
      {props.row.map((cell) => (
        <TableCell key={cell.id} align={cell.align}>
          {cell.value}
        </TableCell>
      ))}
    </TableRow>
  );
}

BasicRow.propTypes = {
  row: PropTypes.array,
};
