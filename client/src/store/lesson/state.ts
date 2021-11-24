import { LessonStateEnum } from "pages/lesson/models/lessonState";
import { Repeat } from "pages/lesson/models/repeat";

export default interface LessonState {
  repeats: Repeat[];
  lessonState: LessonStateEnum;
  isCorrect: boolean | null;
  answer: string;
  cardsCount: number;
}

export const initialState: LessonState = {
  repeats: [],
  lessonState: LessonStateEnum.SetLesson,
  isCorrect: null,
  answer: "",
  cardsCount: 0,
};
