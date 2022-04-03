export interface RegisterFormModel {
  userName: string;
  email: string;
  password: string;
  passwordConfirmation: string;
}

export const initialValue: RegisterFormModel = {
  userName: "",
  email: "",
  password: "",
  passwordConfirmation: "",
};
