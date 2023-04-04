import { ApiResponse } from "common/models/response";
import { Repeat } from "pages/lesson/models/repeat";
import * as queries from "../queries";
import http, { createErrorResponse, createResponse } from "./httpBase";

export async function repeats(request: queries.RepeatsQuery): Promise<Repeat[]> {
  const response = await http.get<Repeat[]>(`/repeats${getQuery(request)}`);
  return response.data;
}

export async function repeatsCount(
  request: queries.RepeatsCountQuery
): Promise<ApiResponse<number>> {
  try {
    getQuery(request);
    const response = await http.get<number>(`/repeats/count${getQuery(request)}`);
    return createResponse(response.data);
  } catch (e: any) {
    return createErrorResponse("");
  }
}

export function getQuery(request: any): string {
  let query = "?";
  // tslint:disable-next-line:forin
  for (const prop in request) {
    if (request[prop] instanceof Array) {
      const array = request[prop] as [];

      // tslint:disable-next-line:prefer-for-of
      for (let i = 0; i < array.length; i++) {
        query += `${prop}=${array[i]}&`;
      }
      continue;
    }
    query += `${prop}=${request[prop]}&`;
  }
  return query;
}
