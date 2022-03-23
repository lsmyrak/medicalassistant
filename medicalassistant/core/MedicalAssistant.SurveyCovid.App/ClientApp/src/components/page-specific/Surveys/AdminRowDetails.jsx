import React, { useMemo } from "react";
import PropTypes from "prop-types";
import { CustomTable, ActionCell, CollapseRow, BasicRow } from "../../common";
import { makeStyles } from "@material-ui/core/styles";
import { green, pink, blue, purple, red } from "@material-ui/core/colors";

import Typography from "@material-ui/core/Typography";
import Box from "@material-ui/core/Box";
import List from "@material-ui/core/List";
import ListItem from "@material-ui/core/ListItem";
import ListItemText from "@material-ui/core/ListItemText";
import ListItemAvatar from "@material-ui/core/ListItemAvatar";
import Avatar from "@material-ui/core/Avatar";
import DoneIcon from "@material-ui/icons/Done";
import GetAppIcon from "@material-ui/icons/GetApp";
import LocalHospitalIcon from "@material-ui/icons/LocalHospital";
import PersonIcon from "@material-ui/icons/Person";
import Grid from "@material-ui/core/Grid";

const useStyles = makeStyles((theme) => ({
  root: {
    display: "flex",
    "& > *": {
      margin: theme.spacing(1),
    },
  },
  pink: {
    color: theme.palette.getContrastText(pink[500]),
    backgroundColor: pink[500],
  },
  green: {
    color: "#fff",
    backgroundColor: green[500],
  },
  blue: {
    color: "#fff",
    backgroundColor: blue[500],
  },
  purple: {
    color: "#fff",
    backgroundColor: purple[500],
  },
  red: {
    color: "#fff",
    backgroundColor: red[500],
  },
}));

export function AdminRowDetails(props) {
  const { row } = props;
  const classes = useStyles();

  return (
    <Box margin={1}>
      <Typography variant="h6" gutterBottom component="div">
        Dane szczgółowe
      </Typography>
      <Grid container spacing={3}>
        <Grid item xs={6}>
          <List dense={true}>
            <ListItem>
              <ListItemAvatar>
                <Avatar className={classes.purple}>
                  <PersonIcon />
                </Avatar>
              </ListItemAvatar>
              <ListItemText primary="Użytkownik" secondary="Jan Kowalski" />
            </ListItem>
            <ListItem>
              <ListItemAvatar>
                <Avatar className={classes.blue}>
                  <LocalHospitalIcon />
                </Avatar>
              </ListItemAvatar>
              <ListItemText
                primary={row.product.productName}
                secondary={`Kod: ${row.product.productCode}; wartość jednostki: ${row.product.unitValue} liczba: ${row.product.unitsCount}`}
              />
            </ListItem>
          </List>
        </Grid>
        <Grid item xs={6}>
          <List dense={true}>
            <ListItem>
              <ListItemAvatar>
                <Avatar className={classes.green}>
                  <DoneIcon />
                </Avatar>
              </ListItemAvatar>
              <ListItemText primary="Status" secondary={row.status != false ? "Aktywna" : "Nieaktywna"} />
            </ListItem>
            <ListItem>
              <ListItemAvatar>
                <Avatar className={classes.red}>
                  <GetAppIcon />
                </Avatar>
              </ListItemAvatar>
              <ListItemText
                primary="Export do excela"
                secondary={row.export != false ? "Wyeksportowana" : "Niewyksportowana"}
              />
            </ListItem>
          </List>
        </Grid>
      </Grid>
    </Box>
  );
}

AdminRowDetails.propTypes = {
  row: PropTypes.object,
};
