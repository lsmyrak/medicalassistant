import React from "react";
import PropTypes from "prop-types";

import { AuthService, AuthContext } from "../../shared/authService";

export function AuthProvider(props) {
  return <AuthContext.Provider value={AuthService}>{props.children}</AuthContext.Provider>;
}

AuthProvider.propTypes = {
  children: PropTypes.node,
};
