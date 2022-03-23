import * as Yup from "yup";

export const departmentSchema = Yup.object().shape({
  name: Yup.string().required("Pole nazwa jest wymagane"),
});
