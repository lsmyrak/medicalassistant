/* eslint-disable react/prop-types */
import React, { useCallback, useState } from "react";
import { Formik } from "formik";
import { makeStyles } from "@material-ui/core/styles";

import Card from "@material-ui/core/Card";
import AppBar from "@material-ui/core/AppBar";
import Toolbar from "@material-ui/core/Toolbar";
import Typography from "@material-ui/core/Typography";
import TextField from "@material-ui/core/TextField";
import IconButton from "@material-ui/core/IconButton";
import Input from "@material-ui/core/Input";
import InputLabel from "@material-ui/core/InputLabel";
import InputAdornment from "@material-ui/core/InputAdornment";
import FormHelperText from "@material-ui/core/FormHelperText";
import FormControl from "@material-ui/core/FormControl";
import Visibility from "@material-ui/icons/Visibility";
import VisibilityOff from "@material-ui/icons/VisibilityOff";
import CardContent from "@material-ui/core/CardContent";
import CardActions from "@material-ui/core/CardActions";
import FormGroup from "@material-ui/core/FormGroup";
import Grid from "@material-ui/core/Grid";
import Button from "@material-ui/core/Button";
import Box from "@material-ui/core/Box";
import LockOpenIcon from "@material-ui/icons/LockOpen";
import ErrorOutlineIcon from "@material-ui/icons/ErrorOutline";

import { useAuthService } from "../shared/hooks/useAuthService";
import { useHistory, useLocation, Redirect } from "react-router-dom";

const useStyles = makeStyles((theme) => ({
  root: {
    flexGrow: 1,
    width: "30%",
    height: "auto",
    position: "absolute",
    top: "20%",
    left: "35%",
  },
  button: {
    flexGrow: 1,
  },
  margin: {
    margin: theme.spacing(3),
  },
  menuButton: {
    marginRight: theme.spacing(2),
  },
  title: {
    flexGrow: 1,
  },
  toolBar: {
    height: 60,
    padding: theme.spacing(2, 2, 0, 2),
  },
}));

export function AuthPage() {
  const classes = useStyles();
  const authService = useAuthService();
  const history = useHistory();
  const location = useLocation();
  const [showPassword, setShowPassword] = useState(false);
  const [loginSuccess, setLoginSuccess] = useState(true);
  const [errorMesage, seteErrorMesage] = useState("");
  const entryUrl = location.state?.from || "/";

  const handleClickShowPassword = () => {
    setShowPassword(!showPassword);
  };

  const login = useCallback(
    async (email, password, setSubmitting) => {
      let result;
      try {
        result = await authService.login(email, password);
      } catch (err) {
        console.warn(err);
      } finally {
        setSubmitting(false);
        if (result.success) {
          setLoginSuccess(true);
          history.replace(entryUrl);
        } else {
          setLoginSuccess(false);
          seteErrorMesage(result.errorMesage);
        }
      }
    },
    [authService, history, entryUrl],
  );

  const handleMouseDownPassword = (event) => {
    event.preventDefault();
  };

  if (authService.isAuthenticated()) {
    return <Redirect to="/" />;
  }

  return (
    <Box boxShadow={3} borderRadius="borderRadius">
      <Card className={classes.root}>
        <Box className={classes.toolBar}>
          <AppBar position="relative" color={loginSuccess ? "primary" : "secondary"}>
            <Toolbar>
              <Typography variant="h6" className={classes.title}>
                {loginSuccess ? "Zaloguj się" : errorMesage}
              </Typography>
              {loginSuccess ? <LockOpenIcon /> : <ErrorOutlineIcon />}
            </Toolbar>
          </AppBar>
        </Box>
        <div>
          <Formik
            initialValues={{ email: "", password: "" }}
            validate={(values) => {
              const errors = {};

              if (!values.email) {
                errors.email = "Required";
              } else if (!/^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}$/i.test(values.email)) {
                errors.email = "Niepoprawny adres email.";
              }

              if (!values.password) {
                errors.password = "Required";
              } else if (values.password.length < 6) {
                errors.password = "Hasło powinno zawierać min 6 znaków.";
              }

              return errors;
            }}
            onSubmit={(values, { setSubmitting }) => {
              login(values.email, values.password, setSubmitting);
            }}
          >
            {({ values, errors, touched, handleChange, handleBlur, handleSubmit, isSubmitting }) => (
              <form className="form" onSubmit={handleSubmit} noValidate>
                <CardContent>
                  <FormGroup>
                    <Grid container spacing={1}>
                      <TextField
                        error={errors.email && touched.email}
                        fullWidth
                        className={classes.margin}
                        id="standard-error-helper-text"
                        label="Email ..."
                        type="email"
                        name="email"
                        helperText={errors.email && touched.email ? errors.email : null}
                        onChange={handleChange}
                        onBlur={handleBlur}
                        value={values.email}
                      />
                    </Grid>
                  </FormGroup>

                  <FormGroup>
                    <Grid container spacing={1}>
                      <FormControl fullWidth className={classes.margin} error={errors.password && touched.password}>
                        <InputLabel htmlFor="standard-adornment-password">Hasło</InputLabel>
                        <Input
                          id="standard-adornment-password"
                          type={showPassword ? "text" : "password"}
                          name="password"
                          onChange={handleChange}
                          onBlur={handleBlur}
                          value={values.password}
                          endAdornment={
                            <InputAdornment position="end">
                              <IconButton
                                aria-label="toggle password visibility"
                                onClick={handleClickShowPassword}
                                onMouseDown={handleMouseDownPassword}
                              >
                                {showPassword ? <Visibility /> : <VisibilityOff />}
                              </IconButton>
                            </InputAdornment>
                          }
                        />
                        <FormHelperText
                          error={errors.password && touched.password}
                          id="standard-adornment-password-error-text"
                        >
                          {errors.password && touched.password ? errors.password : null}
                        </FormHelperText>
                      </FormControl>
                    </Grid>
                  </FormGroup>
                </CardContent>
                <CardActions>
                  <Button
                    className={classes.button}
                    variant="contained"
                    type="submit"
                    disabled={isSubmitting}
                    // color="primary"
                    style={{ marginLeft: "50px", marginRight: "50px", marginBottom: "40px" }}
                  >
                    <strong>Zaloguj</strong>
                  </Button>
                </CardActions>
              </form>
            )}
          </Formik>
        </div>
      </Card>
    </Box>
  );
}
