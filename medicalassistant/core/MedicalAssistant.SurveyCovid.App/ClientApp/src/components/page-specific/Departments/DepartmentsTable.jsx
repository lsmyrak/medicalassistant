import React, { useMemo } from "react";
import PropTypes from "prop-types";
import { CustomTable, ActionCell, BasicRow } from "../../common";

const headCells = [
  { id: "id", disablePadding: true, label: "GUID" },
  { id: "name", disablePadding: false, label: "Nazwa jednostki" },
  { id: "status", disablePadding: false, label: "Status" },
  { id: "actions", disablePadding: false, label: "Akcje" },
];

const createDataCells = (row, onEdit, onDelete) => [
  { id: "id", value: row.id, align: "center" },
  { id: "name", value: row.name, align: "left" },
  { id: "status", value: row.status === false ? "Nieaktywna" : "Aktywna", align: "center" },
  {
    id: "action",
    value: <ActionCell onEdit={() => onEdit(row)} onDelete={() => onDelete(row)} id={row.id} />,
    align: "center",
  },
];
export function DepartmentsTable(props) {
  const {
    departments,
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
  } = props;
  const rows = useMemo(
    () =>
      departments.map((row) => {
        return <BasicRow key={row.id} row={createDataCells(row, onEdit, onDelete)} />;
      }),
    [props.departments],
  );

  return (
    <CustomTable
      headCells={headCells}
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

DepartmentsTable.propTypes = {
  departments: PropTypes.array,
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
};
