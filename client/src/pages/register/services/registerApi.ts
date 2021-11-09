import { ApiResponse } from "common/models/response";
import http from "common/services/http/http";
import RegisterRequest from "../models/registerRequest";
import RegisterResponse from "../models/registerResponse";

export default async function login(
  userName: string,
  email: string,
  password: string,
  passwordConfirmation: string
): Promise<ApiResponse<RegisterResponse>> {
  const request = {
    userName,
    email,
    password,
    passwordConfirmation,
  } as RegisterRequest;
  const response = await http.put<ApiResponse<RegisterResponse>>(
    "/users/register",
    request
  );
  return response.data;
}
