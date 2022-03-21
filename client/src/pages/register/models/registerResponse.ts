export interface RegisterResponse {
  responseCode: RegisterResponseCode;
  id: string;
}
export enum RegisterResponseCode {
  Successful = 0,
  UserNameAlreadyOccupied = 1,
  EmailAlreadyOccupied = 2,
}
