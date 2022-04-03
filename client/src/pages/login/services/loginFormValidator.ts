import { LoginFormModel } from "../models";

export function validate(values: LoginFormModel): LoginFormModel {
  const errors = {} as LoginFormModel;
  if (!values.userName?.length) {
    errors.userName = "Field is required";
  }
  if (!values.password?.length) {
    errors.password = "Field is required";
  }
  return errors;
}
