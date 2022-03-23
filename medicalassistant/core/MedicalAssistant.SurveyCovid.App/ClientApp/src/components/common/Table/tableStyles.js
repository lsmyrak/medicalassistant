import { lighten, makeStyles } from "@material-ui/core/styles";

export const tableStyles = makeStyles((theme) => ({
  root: {
    width: "100%",
  },
  paper: {
    width: "100%",
    marginBottom: theme.spacing(2),
  },
  table: {
    minWidth: 750,
  },
  visuallyHidden: {
    border: 0,
    clip: "rect(0 0 0 0)",
    height: 1,
    margin: -1,
    overflow: "hidden",
    padding: 0,
    position: "absolute",
    top: 20,
    width: 1,
  },
  text: {
    marginRight: "1.25rem",
    marginTop: "0.5rem",
  },
}));

export const paginationRowStyles = makeStyles(() => ({
  padding: {
    display: "flex",
    justifyContent: "space-around",
  },
  text: {
    marginRight: "1.25rem",
    marginTop: "0.5rem",
  },
}));

export const toolbarStyles = makeStyles((theme) => ({
  root: {
    flex: "1 1 100%",
    paddingLeft: theme.spacing(2),
    paddingRight: theme.spacing(1),
  },
  highlight:
    theme.palette.type === "light"
      ? {
          color: theme.palette.secondary.main,
          backgroundColor: lighten(theme.palette.secondary.light, 0.85),
        }
      : {
          color: theme.palette.text.primary,
          backgroundColor: theme.palette.secondary.dark,
        },
  title: {
    flex: "1 1 100%",
    // fontWeight: "500",
    // fontSize: "1.25rem",
    // fontFamily: '"Roboto", "Helvetica", "Arial", sans-serif',
    // lineHeight: 1.6,
    // letterSpacing: "0.0075em",
    // marginLeft: "1.25rem",
  },
}));
