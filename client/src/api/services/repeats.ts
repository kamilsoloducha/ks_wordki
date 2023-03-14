import { ApiResponse } from "common/models/response";
import { Repeat } from "pages/lesson/models/repeat";
import * as queries from "../queries";
import http, { createErrorResponse, createResponse } from "./httpBase";

export async function repeats(request: queries.RepeatsQuery): Promise<Repeat[]> {
  const response = await http.get<Repeat[]>(
    `/repeats?GroupId=${request.groupId}&Count=${request.count}&Languages=${request.questionLanguage}&LessonIncluded=${request.lessonIncluded}`
  );
  return response.data;
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
