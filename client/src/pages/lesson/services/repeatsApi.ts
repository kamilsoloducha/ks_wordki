import http from "common/services/http/http";
import GetRepeatsCountRequest from "../models/getRepeatsCountRequest";
import GetRepeatsRequest from "../models/getRepeatsRequest";
import { GetRepeatsResponse } from "../models/getRepeatsResponse";
import RegisterAnswerRequest from "../models/registerAnswerRequest";
import StartLessonRequest from "../models/startLessonRequest";
import TickCardRequest from "../models/tickCardRequest";

export async function repeats(request: GetRepeatsRequest) {
  const response = await http.get<GetRepeatsResponse>(`/repeats/`, {
    params: request,
  });
  return { data: response.data };
}

export async function repeatsCount(request: GetRepeatsCountRequest) {
  const response = await http.get<number>(`/repeats/count`, {
    params: request,
  });
  return { data: response.data };
}

export async function startLesson(request: StartLessonRequest) {
  const response = await http.post<{}>("/lesson/start", request);
  return { data: response.data };
}

export async function registerAnswer(
  userId: string,
  groupId: string,
  cardId: string,
  side: number,
  result: number
) {
  const request = {
    userId,
    groupId,
    cardId,
    side,
    result,
  } as RegisterAnswerRequest;
  const response = await http.post<{}>("/lesson/answer", request);
  return { data: response.data };
}

export async function tickCard(request: TickCardRequest) {
  const response = await http.put<{}>("/cards/tick", request);
  return { data: response.data };
}
