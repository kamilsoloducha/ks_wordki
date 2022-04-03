import { LoginResponseCode } from "./loginResponseCode";

export interface LoginResponse {
  responseCode: LoginResponseCode;
  id: string;
  token: string;
  creatingDateTime: string;
  expirationDateTime: string;
}
