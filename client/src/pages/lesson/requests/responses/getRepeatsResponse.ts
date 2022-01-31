import { Repeat } from "pages/lesson/models/repeat";

export interface GetRepeatsResponse {
  repeats: Repeat[];
}

export interface RepeatDto {
  sideId: number;
  cardId: number;
  questionSide: number;
  question: string;
  questionExample: string;
  questionDrawer: number;
  answer: string;
  answerExample: string;
  answerSide: number;
  frontLanguage: number;
  backLanguage: number;
  comment: string;
  groupId: string;
}
