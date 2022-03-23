import React, { useCallback, useState, useEffect } from "react";
import uuid from "uuid";
import { CustomModal } from "../components/common";
import { SurveyCovidApiClient } from "../shared/api/surveyCovidApiClient";
import { DepartmentsTable, DepartmentForm } from "../components/page-specific/Departments";
import { Typography } from "@material-ui/core";

const DefaultDepartment = {
  id: "",
  name: "",
  status: true,
};

export function Departments() {
  const [departments, setDepartments] = useState([]);
  const [open, setOpen] = useState(false);
  const [department, setDepartment] = useState();
  const [rowsPerPage, setRowsPerPage] = useState(5);
  const [totalRows, setTotalRows] = useState(0);
  const [page, setPage] = useState(1);
  const [column, setColumn] = useState("name");
  const [order, setOrder] = useState("asc");

  const handleOpen = () => {
    setOpen(true);
  };

  const handleClose = () => {
    setOpen(false);
  };

  const getDepartments = useCallback(async () => {
    const data = (
      await SurveyCovidApiClient.department.getDepartments({
        page: page,
        pageSize: rowsPerPage,
        sort: `${column} ${order}`,
      })
    ).data;
    setDepartments(data.data);
    setTotalRows(data.totalCount);
  }, [setDepartments, rowsPerPage, page, column, order]);

  const onDepartmentSave = useCallback(
    async (value) => {
      if (value.id !== "") {
        await SurveyCovidApiClient.department.updateDepartment(value);
      } else {
        await SurveyCovidApiClient.department.addDepartment({ ...value, id: uuid() });
      }
      await getDepartments();
    },
    [getDepartments],
  );

  const handleEdit = (value) => {
    setDepartment(value);
    handleOpen();
  };
  const handleDelete = useCallback(
    async (value) => {
      await SurveyCovidApiClient.department.updateDepartment({ ...value, status: false });
      await getDepartments();
    },
    [getDepartments],
  );
  const handleAdd = () => {
    setDepartment(DefaultDepartment);
    handleOpen();
  };
  const handleChangeRowsPerPage = (event) => {
    setRowsPerPage(parseInt(event.target.value, 10));
  };

  const handlePageChange = (event, currentPage) => {
    setPage(currentPage);
  };

  const handleSortChange = (property) => (event) => {
    console.log(property);
    setOrder(column === property && order === "asc" ? "desc" : "asc");
    setColumn(property);
  };

  useEffect(() => {
    getDepartments();
  }, [getDepartments]);

  return (
    <>
      <div style={{ marginBottom: "30px" }}>
        <Typography variant="h4" id="tableTitle" component="div">
          Lista jednostek
        </Typography>
      </div>
      <DepartmentsTable
        departments={departments}
        onEdit={handleEdit}
        onDelete={handleDelete}
        onAdd={handleAdd}
        rowsPerPage={rowsPerPage}
        onRowsPerPageChange={handleChangeRowsPerPage}
        totalRows={totalRows}
        onPageChange={handlePageChange}
        page={page}
        column={column}
        order={order}
        onSortChange={handleSortChange}
      />
      <CustomModal open={open} onClose={handleClose}>
        <DepartmentForm department={department} onDepartmentSave={onDepartmentSave} onClose={handleClose} />
      </CustomModal>
    </>
  );
}
