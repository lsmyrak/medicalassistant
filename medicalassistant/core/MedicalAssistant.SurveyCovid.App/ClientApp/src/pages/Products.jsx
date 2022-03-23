import React, { useCallback, useState, useEffect } from "react";
import uuid from "uuid";
import { CustomModal } from "../components/common";
import { SurveyCovidApiClient } from "../shared/api/surveyCovidApiClient";
import { ProductsTable } from "../components/page-specific";
import { Typography } from "@material-ui/core";

const DefaultProduct = {
  id: "",
  description: "",
  productCode: "",
  productName: "",
  unitsCount: 0,
  unitValue: 0,
  status: true,
};

export function Products() {
  const [products, setProducts] = useState([]);
  const [open, setOpen] = useState(false);
  const [product, setProduct] = useState();
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

  const getProducts = useCallback(async () => {
    const data = (
      await SurveyCovidApiClient.product.getProducts({
        page: page,
        pageSize: rowsPerPage,
        sort: `${column} ${order}`,
      })
    ).data;
    setProducts(data.data);
    setTotalRows(data.totalCount);
  }, [setProducts, rowsPerPage, page, column, order]);

  const onProductSave = useCallback(
    async (value) => {
      if (value.id !== "") {
        await SurveyCovidApiClient.product.updateProduct(value);
      } else {
        await SurveyCovidApiClient.product.addProduct({ ...value, id: uuid() });
      }
      await getProducts();
    },
    [getProducts],
  );

  const handleEdit = (value) => {
    setProduct(value);
    handleOpen();
  };
  const handleDelete = useCallback(
    async (value) => {
      await SurveyCovidApiClient.product.updateProduct({ ...value, status: false });
      await getProducts();
    },
    [getProducts],
  );
  const handleAdd = () => {
    setProduct(DefaultProduct);
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
    getProducts();
  }, [getProducts]);

  return (
    <>
      <div style={{ marginBottom: "30px" }}>
        <Typography variant="h4" id="tableTitle" component="div">
          Lista produktów
        </Typography>
      </div>
      <ProductsTable
        products={products}
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
      {/* <CustomModal open={open} onClose={handleClose}>
        <DepartmentForm department={department} onDepartmentSave={onDepartmentSave} onClose={handleClose} />
      </CustomModal> */}
    </>
  );
}
