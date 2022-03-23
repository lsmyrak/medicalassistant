import React from "react";
import PropTypes from "prop-types";

import TableCell from "@material-ui/core/TableCell";
import IconButton from "@material-ui/core/IconButton";
import DeleteIcon from "@material-ui/icons/Delete";
import EditIcon from "@material-ui/icons/Edit";

export function ActionCell(props) {
  return (
    <>
      <IconButton color="primary" aria-label="upload picture" component="span" onClick={() => props.onEdit(props.id)}>
        <EditIcon />
      </IconButton>
      <IconButton
        color="secondary"
        aria-label="upload picture"
        component="span"
        onClick={() => props.onDelete(props.id)}
      >
        <DeleteIcon />
      </IconButton>
    </>
  );
}

ActionCell.propTypes = {
  onEdit: PropTypes.func,
  onDelete: PropTypes.func,
  id: PropTypes.string,
};
