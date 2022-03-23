import { config } from "@fortawesome/fontawesome-svg-core";
import axios from "axios";
import { USER_URLS } from "../constants";

export class UserApiClient {
  static async login(request) {
    let result = {
      success: true,
      statusCode: 0,
      payload: "",
      error: {
        errorCode: 0,
        message: "",
      },
    };
    await axios
      .post(USER_URLS.LOGIN, request)
      .then((response) => {
        result = { ...result, statusCode: response.status, payload: response.data };
      })
      .catch(
        (error) =>
          (result = {
            ...result,
            error: { errorCode: error.response.status, message: error.response.data },
            success: false,
          }),
      );
    return result;
  }

  static async register(request) {
    return await axios.post(USER_URLS.REGISTER, request);
  }

  static async refreshToken(request) {
    let result = {
      success: true,
      statusCode: 0,
      payload: "",
      error: {
        errorCode: 0,
        message: "",
      },
    };
    await axios
      .post(USER_URLS.REFRESH_TOKEN, request, {
        ...config,
        params: {
          ...config?.params,
          refreshToken: request,
        },
      })
      .then((response) => {
        result = { ...result, statusCode: response.status, payload: response.data };
      })
      .catch(
        (error) =>
          (result = {
            ...result,
            error: { errorCode: error.response.status, message: error.response.data },
            success: false,
          }),
      );
    return result;
  }
}
