import http from "common/services/http/http";
import { GetRepeatsResponse } from "../models/getRepeatsResponse";
import RegisterAnswerRequest from "../models/registerAnswerRequest";
import StartLessonRequest from "../models/startLessonRequest";

export async function repeats(count: number) {
  const response = await http.get<GetRepeatsResponse>(`/repeats/${count}`);
  return { data: response.data };
}

export async function repeatsCount() {
  const response = await http.get<number>(`/repeats/count`);
  return { data: response.data };
}

export async function startLesson(userId: string) {
  const request = { userId } as StartLessonRequest;
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
