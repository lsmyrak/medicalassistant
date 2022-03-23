import React, { useCallback, useState, useEffect } from "react";
import uuid from "uuid";
import { Role } from "../shared/constants";
import { CustomModal } from "../components/common";
import { SurveyCovidApiClient } from "../shared/api/surveyCovidApiClient";
import { SurveysTable } from "../components/page-specific/Surveys";
import { Typography } from "@material-ui/core";

import { useAuthService } from "../shared/hooks/useAuthService";

const DefaultDepartment = {
  id: "",
  name: "",
  status: true,
};

export function Surveys() {
  const [surveys, setSurveys] = useState([]);
  const [open, setOpen] = useState(false);
  const [survey, setSurvey] = useState();
  const [rowsPerPage, setRowsPerPage] = useState(10);
  const [totalRows, setTotalRows] = useState(0);
  const [page, setPage] = useState(1);
  const [column, setColumn] = useState("entryDate");
  const [order, setOrder] = useState("desc");
  const authService = useAuthService();

  const handleOpen = () => {
    setOpen(true);
  };

  const handleClose = () => {
    setOpen(false);
  };

  const getSurveys = useCallback(async () => {
    const data = (
      await SurveyCovidApiClient.survey.getSurveys({
        page: page,
        pageSize: rowsPerPage,
        sort: `${column} ${order}`,
      })
    ).data;
    console.log(data);
    setSurveys(data.data);
    setTotalRows(data.totalCount);
  }, [setSurveys, rowsPerPage, page, column, order]);

  const onSurveySave = useCallback(
    async (value) => {
      if (value.id !== "") {
        await SurveyCovidApiClient.survey.updateSurvey(value);
      } else {
        await SurveyCovidApiClient.survey.addSurvey({ ...value, id: uuid() });
      }
      await getSurveys();
    },
    [getSurveys],
  );

  const handleEdit = (value) => {
    setSurvey(value);
    handleOpen();
  };
  const handleDelete = useCallback(
    async (value) => {
      await SurveyCovidApiClient.department.updateSurvey({ ...value, status: false });
      await getSurveys();
    },
    [getSurveys],
  );
  const handleAdd = () => {
    setSurvey(DefaultDepartment);
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
    getSurveys();
  }, [getSurveys]);

  return (
    <>
      <div style={{ marginBottom: "30px" }}>
        <Typography variant="h4" id="tableTitle" component="div">
          Lista Ankiet
        </Typography>
      </div>
      <SurveysTable
        surveys={surveys}
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
        isAdmin={authService.hasRequiredRole([Role.ADMIN])}
      />
      {/* <CustomModal open={open} onClose={handleClose}>
        <DepartmentForm department={department} onDepartmentSave={onDepartmentSave} onClose={handleClose} />
      </CustomModal> */}
    </>
  );
}
