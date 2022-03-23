import React, { useCallback } from "react";
import { UserApiClient } from "./api";

class AuthServiceProvider {
  // constructor() {
  //   // const publicUrl = window.location.origin;
  //   // const hostname = window.location.hostname;
  //   // const authIssuer = "";
  // }

  signinRedirectCallback = () => {};

  getUser = async () => {};

  getUserProfile = () => {
    const storedState = this.getStoredState();
    return { userId: storedState?.userId, userName: storedState?.name, role: storedState?.role };
  };

  getToken = async () => {
    let storedState = this.getStoredState();
    if (storedState && Date.parse(storedState.tokenExpires) > Date.now()) {
      return storedState?.token;
    }
    const response = await UserApiClient.refreshToken(storedState.refreshToken);
    if (response.success) {
      sessionStorage.setItem("user:MedicalAssistant", JSON.stringify(response.payload));
    }

    storedState = this.getStoredState();
    return storedState?.token;
  };

  signinRedirect = () => {};

  navigateToScreen = () => {};

  isAuthenticated = () => {
    const storedState = this.getStoredState();
    return !!storedState && !!storedState.token;
  };

  hasRequiredRole = (requiredRoles = []) => {
    const storedState = this.getStoredState();
    return (
      this.isAuthenticated() &&
      storedState?.role &&
      (requiredRoles.length <= 0 || requiredRoles.includes(storedState.role))
    );
  };

  signinSilentCallback = () => {};

  logout = () => sessionStorage.removeItem("user:MedicalAssistant");

  login = async (login, password) => {
    const response = await UserApiClient.login({ login: login, password: password });
    if (response.success) {
      sessionStorage.setItem("user:MedicalAssistant", JSON.stringify(response.payload));
    }

    return { success: response.success, errorMesage: response.error.message };
  };

  signoutRedirectCallback = () => {};

  getStoredState = () => {
    const storedState = sessionStorage.getItem("user:MedicalAssistant");
    if (!storedState) {
      return undefined;
    }
    return JSON.parse(storedState);
  };
}

export const AuthService = new AuthServiceProvider();
export const AuthContext = React.createContext(AuthService);
export const { Consumer: AuthConsumer } = AuthContext;
