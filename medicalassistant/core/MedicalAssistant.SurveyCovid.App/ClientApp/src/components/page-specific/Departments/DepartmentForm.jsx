import React from "react";
import { Formik } from "formik";

import PropTypes from "prop-types";
import { departmentSchema } from "./schema";

import { Button, FormGroup } from "@material-ui/core";
import TextField from "@material-ui/core/TextField";
import FormControlLabel from "@material-ui/core/FormControlLabel";
import Checkbox from "@material-ui/core/Checkbox";
import CardActions from "@material-ui/core/CardActions";

export function DepartmentForm(props) {
  const { department, onDepartmentSave, onClose } = props;

  return (
    <Formik
      initialValues={department}
      validationSchema={departmentSchema}
      onSubmit={(values) => {
        onDepartmentSave(values);
        onClose();
      }}
    >
      {({ handleSubmit, values, handleChange, errors, touched }) => (
        <form>
          <FormGroup>
            <TextField
              id="standard-error-helper-text"
              name="name"
              helperText={errors.name}
              placeholder="Nazwa jednostki"
              onChange={handleChange}
              value={values.name}
              error={!!errors.name && touched.name}
            />
          </FormGroup>
          <FormGroup>
            <FormControlLabel
              checked={values.status}
              control={<Checkbox name="status" onChange={handleChange} />}
              label="Aktywna"
            />
          </FormGroup>
          <CardActions>
            <Button type="submit" size="small" onClick={handleSubmit}>
              Zapisz
            </Button>
            <Button size="small" onClick={onClose}>
              Anuluj
            </Button>
          </CardActions>
        </form>
      )}
    </Formik>
  );
}

DepartmentForm.propTypes = {
  department: PropTypes.object,
  onDepartmentSave: PropTypes.func,
  onClose: PropTypes.func,
};
