import React from "react";
import PropTypes from "prop-types";

import TableCell from "@material-ui/core/TableCell";
import TableHead from "@material-ui/core/TableHead";
import TableRow from "@material-ui/core/TableRow";
import TableSortLabel from "@material-ui/core/TableSortLabel";
import Typography from "@material-ui/core/Typography";

export function CustomHeadRow(props) {
  const { classes, headCells, column, order, onSortChange } = props;
  return (
    <TableHead>
      <TableRow>
        {headCells.map((headCell) => (
          <TableCell
            key={headCell.id}
            align={"center"}
            padding={headCell.disablePadding ? "none" : "default"}
            sortDirection={column === headCell.id ? order : false}
          >
            {console.log(column, order)}
            <TableSortLabel
              active={column === headCell.id}
              direction={column === headCell.id ? order : "asc"}
              onClick={onSortChange(headCell.id)}
              className={classes.text}
            >
              <Typography variant="h6" gutterBottom>
                {headCell.label}
              </Typography>
            </TableSortLabel>
          </TableCell>
        ))}
      </TableRow>
    </TableHead>
  );
}

CustomHeadRow.propTypes = {
  classes: PropTypes.object.isRequired,
  headCells: PropTypes.array,
  column: PropTypes.string,
  order: PropTypes.string,
  onSortChange: PropTypes.func,
};
