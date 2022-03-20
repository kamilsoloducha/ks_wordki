import http from "common/services/http/http";
import { LoginRequest, LoginResponse } from "../requests";

export async function login(request: LoginRequest): Promise<LoginResponse> {
  try {
    const response = await http.put<LoginResponse>("/users/login", request);
    return response.data;
  } catch (e: any) {
    return e.response.data as LoginResponse;
  }
}
