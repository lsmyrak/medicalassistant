import React, { useMemo } from "react";
import PropTypes from "prop-types";
import { CustomTable, ActionCell, CollapseRow, BasicRow } from "../../common";

import { AdminRowDetails } from "./AdminRowDetails";

const headCells = [
  { id: "collapse", disablePadding: false, label: "" },
  { id: "entryDate", disablePadding: false, label: "Data wpisu" },
  { id: "pesel", disablePadding: true, label: "Pesel" },
  { id: "anotherDocument", disablePadding: false, label: "Inny dokument" },
  { id: "seriesNumber", disablePadding: false, label: "Seria/Numer" },
  { id: "fromDate", disablePadding: false, label: "Od" },
  { id: "untilDate", disablePadding: false, label: "Do" },
  { id: "place", disablePadding: false, label: "Miejsce" },
  { id: "action", disablePadding: false, label: "Akcje" },
];

const createDataCells = (row, onEdit, onDelete) => [
  { id: "entryDate", value: new Date(row.entryDate).toLocaleDateString("pl-PL"), align: "left" },
  { id: "pesel", value: row.pesel, align: "left" },
  { id: "anotherDocument", value: row.anotherDocument, align: "left" },
  { id: "seriesNumber", value: row.seriesNumber, align: "left" },
  { id: "fromDate", value: new Date(row.fromDate).toLocaleDateString("pl-PL"), align: "left" },
  { id: "untilDate", value: new Date(row.untilDate).toLocaleDateString("pl-PL"), align: "left" },
  { id: "place", value: row.place, align: "left" },
  {
    id: "action",
    value: <ActionCell onEdit={() => onEdit(row)} onDelete={() => onDelete(row)} id={row.id} />,
    align: "left",
  },
];

export function SurveysTable(props) {
  const {
    surveys,
    onEdit,
    onDelete,
    onAdd,
    rowsPerPage,
    onRowsPerPageChange,
    totalRows,
    onPageChange,
    page,
    column,
    order,
    onSortChange,
    isAdmin,
  } = props;

  const rows = useMemo(
    () =>
      surveys.map((row) => {
        return isAdmin ? (
          <CollapseRow key={row.id} row={createDataCells(row, onEdit, onDelete)}>
            <AdminRowDetails row={row} />
          </CollapseRow>
        ) : (
          <BasicRow key={row.id} row={createDataCells(row, onEdit, onDelete)} />
        );
      }),
    [props.surveys],
  );

  return (
    <CustomTable
      headCells={isAdmin ? headCells : headCells.filter((cell) => cell.id != "collapse")}
      onAdd={onAdd}
      rowsPerPage={rowsPerPage}
      onRowsPerPageChange={onRowsPerPageChange}
      totalRows={totalRows}
      onPageChange={onPageChange}
      page={page}
      column={column}
      order={order}
      onSortChange={onSortChange}
    >
      {rows}
    </CustomTable>
  );
}

SurveysTable.propTypes = {
  surveys: PropTypes.array,
  onEdit: PropTypes.func,
  onDelete: PropTypes.func,
  onAdd: PropTypes.func,
  rowsPerPage: PropTypes.number,
  onRowsPerPageChange: PropTypes.func,
  totalRows: PropTypes.number,
  onPageChange: PropTypes.func,
  page: PropTypes.number,
  column: PropTypes.string,
  order: PropTypes.string,
  onSortChange: PropTypes.func,
  isAdmin: PropTypes.bool,
};
