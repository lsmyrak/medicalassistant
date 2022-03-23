import React from "react";
import { paginationRowStyles } from "./tableStyles";
import PropTypes from "prop-types";

import Toolbar from "@material-ui/core/Toolbar";
import Typography from "@material-ui/core/Typography";
import Pagination from "@material-ui/lab/Pagination";
import Grid from "@material-ui/core/Grid";
import TextField from "@material-ui/core/TextField";
import MenuItem from "@material-ui/core/MenuItem";

const pageSize = [
  { value: 5, label: "5" },
  { value: 10, label: "10" },
  { value: 25, label: "25" },
  { value: 50, label: "50" },
];

const getCountPages = (totalRows, rowsPerPage) => {
  return Math.ceil(totalRows / rowsPerPage);
};

export function PaginationRow(props) {
  const { rowsPerPage, onRowsPerPageChange, totalRows, onPageChange } = props;
  const classes = paginationRowStyles();
  return (
    <Toolbar>
      <Grid container spacing={3}>
        <Grid item xs={4} container>
          <Typography variant="subtitle1" gutterBottom className={classes.text}>
            Ilość wierszy na stronie
          </Typography>
          <TextField
            id="outlined-select-currency"
            select
            value={rowsPerPage}
            variant="outlined"
            size="small"
            onChange={onRowsPerPageChange}
          >
            {pageSize.map((option) => (
              <MenuItem key={option.value} value={option.value}>
                {option.label}
              </MenuItem>
            ))}
          </TextField>
        </Grid>
        <Grid item xs={4} align="center">
          <Pagination
            className={classes.padding}
            count={getCountPages(totalRows, rowsPerPage)}
            variant="outlined"
            showFirstButton
            showLastButton
            onChange={onPageChange}
          />
        </Grid>
        <Grid item xs={4} align="right">
          <Typography variant="subtitle1" gutterBottom align="right" spacing={3}>
            Liczba elementów: {totalRows}
          </Typography>
        </Grid>
      </Grid>
    </Toolbar>
  );
}

PaginationRow.propTypes = {
  rowsPerPage: PropTypes.number,
  onRowsPerPageChange: PropTypes.func,
  totalRows: PropTypes.number,
  onPageChange: PropTypes.func,
  page: PropTypes.number,
};
