const USER_PREFIX = "/api/account/";
export const USER_URLS = {
  LOGIN: USER_PREFIX + "login",
  REGISTER: USER_PREFIX + "register",
  REFRESH_TOKEN: USER_PREFIX + "refresh-token",
};

export class SurveyCovidApiClientUrls {
  static Prefix = "/api/";
  static Department = SurveyCovidApiClientUrls.Prefix + "department";
  static AddDepartment = SurveyCovidApiClientUrls.Department + "/add";
  static UpdateDepartment = SurveyCovidApiClientUrls.Department + "/edit";
  static Product = SurveyCovidApiClientUrls.Prefix + "product";
  static AddProduct = SurveyCovidApiClientUrls.Product + "/add";
  static UpdateProduct = SurveyCovidApiClientUrls.Product + "/edit";
  static Survey = SurveyCovidApiClientUrls.Prefix + "survey";
  static Reports = SurveyCovidApiClientUrls.Prefix + "reports";
  static SurveyRaport = SurveyCovidApiClientUrls.Reports + "/survey-report";
}
