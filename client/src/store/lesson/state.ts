import { LessonStateEnum } from "pages/lesson/models/lessonState";
import { Repeat } from "pages/lesson/models/repeat";

export default interface LessonState {
  repeats: Repeat[];
  lessonState: LessonStateEnum;
  isCorrect: boolean | null;
  answer: string;
}

export const initialState: LessonState = {
  repeats: [],
  lessonState: LessonStateEnum.BeforeLoading,
  isCorrect: null,
  answer: "",
};
