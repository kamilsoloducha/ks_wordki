import { LessonStateEnum } from "pages/lesson/models/lessonState";
import { Repeat } from "pages/lesson/models/repeat";
import Results from "pages/lesson/models/results";

export default interface LessonState {
  repeats: Repeat[];
  lessonState: LessonStateEnum;
  isCorrect: boolean | null;
  answer: string;
  cardsCount: number;
  results: Results;
  lessonCount: number;

  lessonType: number;
}

export const initialState: LessonState = {
  repeats: [],
  lessonState: LessonStateEnum.SetLesson,
  isCorrect: null,
  answer: "",
  cardsCount: 0,
  results: {} as Results,
  lessonCount: 0,
  lessonType: 0,
};
