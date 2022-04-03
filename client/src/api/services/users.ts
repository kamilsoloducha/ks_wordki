import * as commands from "../commands";
import * as responses from "../responses";
import http from "./httpBase";

export async function login(request: commands.LoginRequest): Promise<responses.LoginResponse> {
  try {
    const response = await http.put<responses.LoginResponse>("/users/login", request);
    return response.data;
  } catch (e: any) {
    return e.response.data as responses.LoginResponse;
  }
}

export async function register(
  request: commands.RegisterRequest
): Promise<responses.RegisterResponse> {
  try {
    const response = await http.post<responses.RegisterResponse>("/users/register", request);
    return response.data;
  } catch (e) {
    return {} as responses.RegisterResponse;
  }
}
