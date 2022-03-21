import http from "common/services/http/http";
import RegisterRequest from "../models/registerRequest";
import { RegisterResponse } from "../models/registerResponse";

export async function register(request: RegisterRequest): Promise<RegisterResponse> {
  try {
    const response = await http.post<RegisterResponse>("/users/register", request);
    return response.data;
  } catch (e) {
    return {} as RegisterResponse;
  }
}
