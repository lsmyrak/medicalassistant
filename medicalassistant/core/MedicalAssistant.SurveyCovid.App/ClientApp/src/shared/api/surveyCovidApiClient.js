import FileDownload from "js-file-download";

import { SurveyCovidApiClientUrls } from "../constants";
import { ApiClient } from "./baseApiClient";

class DepartmentApiClient extends ApiClient {
  async getDepartments(params) {
    return await ApiClient.getWithToken(SurveyCovidApiClientUrls.Department, {
      params,
    });
  }
  async addDepartment(department) {
    return await ApiClient.postWithToken(SurveyCovidApiClientUrls.AddDepartment, department);
  }
  async updateDepartment(department) {
    return await ApiClient.postWithToken(SurveyCovidApiClientUrls.UpdateDepartment, department);
  }
}

class ProductApiClient extends ApiClient {
  async getProducts(params) {
    return await ApiClient.getWithToken(SurveyCovidApiClientUrls.Product, {
      params,
    });
  }
  async addProduct(product) {
    return await ApiClient.postWithToken(SurveyCovidApiClientUrls.AddProduct, product);
  }
  async updateProduct(product) {
    return await ApiClient.postWithToken(SurveyCovidApiClientUrls.UpdateProduct, product);
  }
}

class SurveyApiClient extends ApiClient {
  async getSurveys(params) {
    return await ApiClient.getWithToken(SurveyCovidApiClientUrls.Survey, {
      params,
    });
  }
}

class ReportsApiClient extends ApiClient {
  async downloadSurveyRaport(filename, from, until) {
    const result = await ApiClient.getWithToken(SurveyCovidApiClientUrls.SurveyRaport, {
      params: { from, until },
      responseType: "blob",
    });
    FileDownload(result.data, filename, "text/xlsx;charset=utf-8");
  }
}

export class SurveyCovidApiClient {
  static department = Object.freeze(new DepartmentApiClient());
  static product = Object.freeze(new ProductApiClient());
  static survey = Object.freeze(new SurveyApiClient());
  static reports = Object.freeze(new ReportsApiClient());
}
