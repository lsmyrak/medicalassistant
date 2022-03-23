import axios from "axios";
import { AuthService } from "../authService";

export class ApiClient {
  static async addToken(config) {
    return await this.addTokenWithProvider(() => AuthService.getToken(), config);
  }

  static async addTokenWithProvider(getToken, config) {
    const token = await getToken();
    if (!token) {
      return config;
    }
    return {
      ...config,
      headers: {
        ...config?.headers,
        Authorization: `Bearer ${token}`,
      },
    };
  }

  static async deleteWithToken(...params) {
    return axios.delete(params[0], await ApiClient.addToken(params[1]));
  }

  static async getWithToken(...params) {
    return axios.get(params[0], await ApiClient.addToken(params[1]));
  }

  static async postWithToken(...params) {
    return axios.post(params[0], params[1], await ApiClient.addToken(params[2]));
  }
}
