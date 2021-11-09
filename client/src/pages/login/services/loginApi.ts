import { ApiResponse } from "common/models/response";
import http from "common/services/http/http";
import LoginRequest from "../models/loginRequest";
import LoginResponse from "../models/loginResponse";

export default async function login(
  userName: string,
  password: string
): Promise<ApiResponse<LoginResponse>> {
  const request = { userName, password } as LoginRequest;
  const response = await http.put<ApiResponse<LoginResponse>>(
    "/users/login",
    request
  );
  return response.data;
}
