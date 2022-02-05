import { AxiosResponse } from "axios";
import http, {
  createErrorResponse,
  createResponse,
} from "common/services/http/http";
import { GetRepeatsResponse } from "../requests/responses/getRepeatsResponse";
import {
  GetRepeatsCountRequest,
  GetRepeatsRequest,
  RegisterAnswerRequest,
  StartLessonRequest,
  TickCardRequest,
} from "../requests";
import { ApiResponse } from "common/models/response";
import { GetGroupsToLessonRequest } from "../requests/getGroupsToLesson";
import { GetGroupToLessonResponse } from "../requests/responses/getGroupsToLessonResponse";

export async function repeats(
  request: GetRepeatsRequest
): Promise<ApiResponse<GetRepeatsResponse>> {
  try {
    const response = await http.get<GetRepeatsResponse>(`/repeats`, {
      params: request,
    });
    return createResponse(response.data);
  } catch (e: any) {
    return createErrorResponse("");
  }
}

export async function repeatsCount(
  request: GetRepeatsCountRequest
): Promise<ApiResponse<number>> {
  try {
    const response = await http.get<number>(`/repeats/count`, {
      params: request,
    });
    return createResponse(response.data);
  } catch (e: any) {
    return createErrorResponse("");
  }
}

export async function startLesson(
  request: StartLessonRequest
): Promise<AxiosResponse<any>> {
  try {
    return await http.post<{}>("/lesson/start", request);
  } catch (e: any) {
    return e;
  }
}

export async function registerAnswer(
  request: RegisterAnswerRequest
): Promise<AxiosResponse<any>> {
  try {
    return await http.post<any>("/lesson/answer", request);
  } catch (e: any) {
    return e;
  }
}

export async function tickCard(
  request: TickCardRequest
): Promise<ApiResponse<any>> {
  try {
    await http.put<any>("/cards/tick", request);
    return {
      isCorrect: true,
    } as ApiResponse<any>;
  } catch (e: any) {
    return e;
  }
}

export async function getGroups(
  request: GetGroupsToLessonRequest
): Promise<ApiResponse<GetGroupToLessonResponse>> {
  try {
    const response = await http.get<GetGroupToLessonResponse>(
      `/groups/lesson/${request.ownerId}`
    );
    return createResponse(response.data);
  } catch (e: any) {
    return createErrorResponse("");
  }
}
