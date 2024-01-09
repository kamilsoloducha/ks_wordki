import { LoginResponseCode } from "./loginResponseCode";

export type LoginResponse = {
  responseCode: LoginResponseCode;
  id: string;
  token: string;
  creatingDateTime: string;
  expirationDateTime: string;
};
