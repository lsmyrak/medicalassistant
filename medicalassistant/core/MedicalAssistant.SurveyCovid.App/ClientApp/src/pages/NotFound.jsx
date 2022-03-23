import React from "react";
// import { useHistory } from "react-router-dom";
// import { makeStyles } from "@material-ui/core/styles";

import { Alert, AlertTitle } from "@material-ui/lab";
import Button from "@material-ui/core/Button";
import Box from "@material-ui/core/Box";

// const useStyles = makeStyles((theme) => ({
//   root: {
//     width: "100%",
//     "& > * + *": {
//       marginTop: theme.spacing(2),
//     },
//   },
// }));

const defaultProps = {
  style: { width: "30%", position: "absolute", top: "20%", left: "35%" },
  boxShadow: 3,
  borderRadius: "borderRadius",
};

export function NotFound() {
  // const classes = useStyles();
  // const history = useHistory();

  return (
    <Box {...defaultProps}>
      <Alert
        boxShadow={3}
        severity="error"
        action={
          <Button color="inherit" size="small">
            POWRÓT
          </Button>
        }
      >
        <AlertTitle className="">
          <strong>Błąd</strong>
        </AlertTitle>
        Wybrana strona — nie istnieje!
      </Alert>
    </Box>
  );
}
