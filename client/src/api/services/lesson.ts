import { AxiosResponse } from "axios";
import * as commands from "../commands";
import http from "./httpBase";

export async function startLesson(
  request: commands.StartLessonRequest
): Promise<AxiosResponse<any>> {
  try {
    return await http.post<{}>("/lesson/start", request);
  } catch (e: any) {
    return e;
  }
}

export async function registerAnswer(
  request: commands.RegisterAnswerRequest
): Promise<AxiosResponse<any>> {
  try {
    return await http.post<any>("/lesson/answer", request);
  } catch (e: any) {
    return e;
  }
}

export async function getLanguages(): Promise<string[]> {
  try {
    var response = await http.get<string[]>("/groups/languages");
    return response.data;
  } catch (e: any) {
    return e;
  }
}
