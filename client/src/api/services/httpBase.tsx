import axios from "axios";
import { ApiResponse } from "common/models/response";

export const API_PATH = process.env["REACT_APP_API_HOST"];
// if (API_PATH === undefined) {
//   console.error("REACT_APP_API_HOST is not set");
// }
const instance = axios.create({
  baseURL: API_PATH,
  headers: {
    "Content-type": "application/json",
  },
});

export default instance;

export function createErrorResponse<T>(message: string): ApiResponse<T> {
  return {
    isCorrect: false,
    error: message,
  } as ApiResponse<T>;
}

export function createResponse<T>(response: T): ApiResponse<T> {
  return {
    isCorrect: true,
    response: response,
  } as ApiResponse<T>;
}
