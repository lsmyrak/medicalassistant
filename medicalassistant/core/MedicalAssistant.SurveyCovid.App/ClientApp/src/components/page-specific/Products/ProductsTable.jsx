import React, { useMemo } from "react";
import PropTypes from "prop-types";
import { CustomTable, ActionCell, BasicRow } from "../../common";

const headCells = [
  { id: "id", disablePadding: true, label: "GUID" },
  { id: "description", disablePadding: false, label: "Opis" },
  { id: "productCode", disablePadding: false, label: "Kod" },
  { id: "productName", disablePadding: false, label: "Nazwa" },
  { id: "unitsCount", disablePadding: false, label: "Liczba" },
  { id: "unitValue", disablePadding: false, label: "Wartość" },
  { id: "status", disablePadding: false, label: "Status" },
  { id: "actions", disablePadding: false, label: "Akcje" },
];

const createDataCells = (row, onEdit, onDelete) => [
  { id: "id", value: row.id, align: "left" },
  { id: "description", value: row.description, align: "left" },
  { id: "productCode", value: row.productCode, align: "left" },
  { id: "productName", value: row.productName, align: "left" },
  { id: "unitsCount", value: row.unitsCount, align: "center" },
  { id: "unitValue", value: row.unitValue.toPrecision(3), align: "center" },
  { id: "status", value: row.status === false ? "Nieaktywny" : "Aktywny", align: "center" },
  {
    id: "action",
    value: <ActionCell onEdit={() => onEdit(row)} onDelete={() => onDelete(row)} id={row.id} />,
    align: "center",
  },
];
export function ProductsTable(props) {
  const {
    products,
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
      products.map((row) => {
        return <BasicRow key={row.id} row={createDataCells(row, onEdit, onDelete)} />;
      }),
    [products],
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

ProductsTable.propTypes = {
  products: PropTypes.array,
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
