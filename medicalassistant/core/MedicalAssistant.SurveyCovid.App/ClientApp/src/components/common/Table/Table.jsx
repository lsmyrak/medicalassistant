import React from "react";
import PropTypes from "prop-types";
import { CustomHeadRow } from "./CustomHeadRow";
import { CustomTableToolbar } from "./CustomTableToolbar";
import { PaginationRow } from "./PaginationRow";
import { tableStyles } from "./tableStyles";
import { Divider } from "@material-ui/core";

import Paper from "@material-ui/core/Paper";
import Table from "@material-ui/core/Table";
import TableBody from "@material-ui/core/TableBody";
import TableContainer from "@material-ui/core/TableContainer";

export function CustomTable(props) {
  const {
    children,
    headCells,
    onAdd,
    rowsPerPage,
    onRowsPerPageChange,
    totalRows,
    onPageChange,
    page,
    column,
    order,
    onSortChange,
  } = props;
  const classes = tableStyles();

  return (
    <>
      <Paper className={classes.paper}>
        <CustomTableToolbar onAdd={onAdd} />
        <Divider variant="middle" />
        <TableContainer>
          <Table className={classes.table} aria-labelledby="tableTitle" size={"small"} aria-label="enhanced table">
            <CustomHeadRow
              classes={classes}
              headCells={headCells}
              column={column}
              order={order}
              onSortChange={onSortChange}
            ></CustomHeadRow>
            <TableBody>{children}</TableBody>
          </Table>
        </TableContainer>
        <PaginationRow
          rowsPerPage={rowsPerPage}
          onRowsPerPageChange={onRowsPerPageChange}
          totalRows={totalRows}
          onPageChange={onPageChange}
          page={page}
        />
      </Paper>
    </>
  );
}

CustomTable.propTypes = {
  children: PropTypes.node,
  headCells: PropTypes.array,
  onAdd: PropTypes.func,
  rowsPerPage: PropTypes.number,
  onRowsPerPageChange: PropTypes.func,
  totalRows: PropTypes.number,
  onPageChange: PropTypes.func,
  page: PropTypes.number,
  column: PropTypes.string,
  order: PropTypes.string,
  onSortChange: PropTypes.func,
};
