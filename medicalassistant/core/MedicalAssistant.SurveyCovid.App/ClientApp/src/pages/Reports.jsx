import { Button } from "@material-ui/core";
import { Formik, Form, Field } from "formik";
import React, { useCallback, useState } from "react";
import DateFnsUtils from "@date-io/date-fns";
import plLocale from "date-fns/locale/pl";
import { SurveyCovidApiClient } from "../shared/api/surveyCovidApiClient";
import Typography from "@material-ui/core/Typography";
import Card from "@material-ui/core/Card";
import CardActions from "@material-ui/core/CardActions";
import CardContent from "@material-ui/core/CardContent";
import { Divider, makeStyles } from "@material-ui/core";
import { CardHeader } from "@material-ui/core";
import { FormGroup } from "@material-ui/core";
import Grid from "@material-ui/core/Grid";
import { KeyboardDatePicker, DatePicker } from "formik-material-ui-pickers";
import { MuiPickersUtilsProvider } from "@material-ui/pickers";
// import { MuiPickersUtilsProvider, KeyboardTimePicker, KeyboardDatePicker } from "@material-ui/pickers";

const useStyles = makeStyles((theme) => ({
  root: {
    borderRadius: 12,
    minWidth: 256,
    textAlign: "center",
  },
  content: {
    flexGrow: 1,
    justifyContent: "center",
  },
  header: {
    textAlign: "center",
    spacing: 10,
  },
  list: {
    padding: "20px",
  },
  button: {
    margin: theme.spacing(1),
  },
  action: {
    display: "flex",
    justifyContent: "space-around",
  },
}));

const validateDateRange = (values) => {
  const errors = {};

  console.log(values);
  if (values.from > values.until) {
    errors.from = "Data początku okresu musi być mniejsza niż data końca okresu";
    errors.until = "Data końca okresu musi być większa niż data początku okresu";
  }

  return errors;
};

export function Reports() {
  const downloadSurveyRaport = useCallback(async (dateRange) => {
    await SurveyCovidApiClient.reports.downloadSurveyRaport(
      "Raport Ankiet Covid.xlsx",
      dateRange.from,
      dateRange.until,
    );
  }, []);
  const classes = useStyles();

  const defaultDateRange = {
    from: new Date(),
    until: new Date(),
  };

  return (
    <div>
      <div style={{ marginBottom: "30px" }}>
        <Typography variant="h4" id="tableTitle" component="div">
          Raporty
        </Typography>
      </div>
      <Formik
        initialValues={defaultDateRange}
        validate={validateDateRange}
        onSubmit={(values, { setSubmitting }) => {
          downloadSurveyRaport(values);
          setSubmitting(false);
        }}
      >
        {({ submitForm, isSubmitting }) => (
          <Form>
            <Card className={classes.root}>
              <CardHeader className={classes.header} title="Raport wprowadzonych ankiet COVID-19" />
              <Divider variant="middle" />
              <CardContent>
                <FormGroup>
                  <Typography variant="h6" id="tableTitle" component="div">
                    Zakres ankiet:
                  </Typography>
                  <MuiPickersUtilsProvider utils={DateFnsUtils} locale={plLocale}>
                    <Grid container className={classes.content} spacing={2}>
                      <Typography
                        variant="h6"
                        id="tableTitle"
                        component="div"
                        style={{ marginTop: "30px", marginRight: "30px" }}
                      >
                        Od:
                      </Typography>
                      <Field
                        component={DatePicker}
                        name="from"
                        label="Pocztek okresu"
                        format="dd/MM/yyyy"
                        margin="normal"
                      />
                      {/* <DatePicker
                        name="from"
                        margin="normal"
                        id="date-picker-dialog-1"
                        label="Pocztek okresu"
                        format="MM/dd/yyyy"
                        KeyboardButtonProps={{
                          "aria-label": "change date",
                        }}
                      /> */}
                      <Typography
                        variant="h6"
                        id="tableTitle"
                        component="div"
                        style={{ marginTop: "30px", marginRight: "30px", marginLeft: "30px" }}
                      >
                        Do:
                      </Typography>
                      <Field
                        component={DatePicker}
                        name="until"
                        label="Koniec okresu"
                        format="dd/MM/yyyy"
                        margin="normal"
                      />
                      {/* <DatePicker
                        name="until"
                        margin="normal"
                        id="date-picker-dialog-2"
                        label="Koniec okresu"
                        format="MM/dd/yyyy"
                        KeyboardButtonProps={{
                          "aria-label": "change date",
                        }}
                      /> */}
                    </Grid>
                  </MuiPickersUtilsProvider>
                </FormGroup>
              </CardContent>
              <Divider variant="middle" />
              <CardActions className={classes.action}>
                <Button
                  className={classes.button}
                  variant="contained"
                  color="primary"
                  disabled={isSubmitting}
                  onClick={submitForm}
                >
                  Pobierz raport
                </Button>
              </CardActions>
            </Card>
          </Form>
        )}
      </Formik>
    </div>
  );
}
