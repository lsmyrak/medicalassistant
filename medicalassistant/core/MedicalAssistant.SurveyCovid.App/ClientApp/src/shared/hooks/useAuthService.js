import { useContext } from "react";

import { AuthContext } from "../authService";

export function useAuthService() {
  const ctx = useContext(AuthContext);

  if (ctx === null) {
    throw new Error("You forgot the provider");
  }

  return ctx;
}
