import { Role, AppUrls } from "../shared/constants";
import { Departments } from "./Departments";
import { Home } from "./Home";
import { Products } from "./Products";
import { Surveys } from "./Surveys";
import { Reports } from "./Reports";

export const privatePages = [
  { path: AppUrls.Index, component: Home, exact: true },
  {
    path: AppUrls.Home,
    component: Home,
    requiredRoles: [Role.ADMIN, Role.USER],
  },
  {
    path: AppUrls.Department,
    component: Departments,
    requiredRoles: [Role.ADMIN],
  },
  {
    path: AppUrls.Product,
    component: Products,
    requiredRoles: [Role.ADMIN],
  },
  {
    path: AppUrls.Survey,
    component: Surveys,
    requiredRoles: [Role.ADMIN, Role.USER],
  },
  {
    path: AppUrls.Reports,
    component: Reports,
    requiredRoles: [Role.ADMIN],
  },
];
