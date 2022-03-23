import React from "react";
import { Route, Redirect } from "react-router-dom";
import { AppUrls } from "../../shared/constants/appUrls";
import { useAuthService } from "../../shared/hooks/useAuthService";
import PropTypes from "prop-types";

export function PrivateRoute({ component: Component, requiredRoles, ...rest }) {
  const authService = useAuthService();
  return (
    <Route
      {...rest}
      render={(routeProps) => {
        if (Component && authService.isAuthenticated()) {
          if (Component && authService.hasRequiredRole(requiredRoles)) {
            return <Component {...routeProps} />;
          } else {
            return <Redirect to={AppUrls.NotFound} />;
          }
        } else {
          return (
            <Redirect
              to={{
                pathname: AppUrls.Auth,
                state: { from: AppUrls.Home },
              }}
            />
          );
        }
      }}
    />
  );
}

PrivateRoute.propTypes = {
  requiredRoles: PropTypes.arrayOf(PropTypes.string),
  component: PropTypes.func,
  rest: PropTypes.object,
};
