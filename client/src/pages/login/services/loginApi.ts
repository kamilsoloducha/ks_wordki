import { ApiResponse } from "common/models/response";
import http from "common/services/http/http";
import { LoginRequest, LoginResponse } from "../requests";

export async function login(request: LoginRequest): Promise<ApiResponse<LoginResponse>> {
  try {
    const response = await http.put<ApiResponse<LoginResponse>>("/users/login", request);
    return response.data;
  } catch (e: any) {
    return e.response.data as ApiResponse<LoginResponse>;
  }
}
