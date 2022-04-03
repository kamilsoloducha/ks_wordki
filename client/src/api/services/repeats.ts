import { ApiResponse } from "common/models/response";
import * as queries from "../queries";
import * as responses from "../responses";
import http, { createErrorResponse, createResponse } from "./httpBase";

export async function repeats(
  request: queries.RepeatsQuery
): Promise<ApiResponse<responses.GetRepeatsResponse>> {
  try {
    const response = await http.post<responses.GetRepeatsResponse>(`/repeats`, request);
    return createResponse(response.data);
  } catch (e: any) {
    return createErrorResponse("");
  }
}

export async function repeatsCount(
  request: queries.repeatsCountQuery
): Promise<ApiResponse<number>> {
  try {
    const response = await http.post<number>(`/repeats/count`, request);
    return createResponse(response.data);
  } catch (e: any) {
    return createErrorResponse("");
  }
}
