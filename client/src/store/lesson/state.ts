import { LessonStateEnum } from "pages/lesson/models/lessonState";
import { Repeat } from "pages/lesson/models/repeat";
import Results from "pages/lesson/models/results";
import { LessonSettings } from "pages/lessonSettings/models/lessonSettings";

export default interface LessonState {
  repeats: Repeat[];
  lessonState: LessonStateEnum;
  isCorrect: boolean | null;
  answer: string;
  cardsCount: number | null;
  lessonCount: number;
  lessonType: number;

  results: Results;
  settings: LessonSettings;
}

export const initialState: LessonState = {
  repeats: [],
  lessonState: LessonStateEnum.SetLesson,
  isCorrect: null,
  answer: "",
  cardsCount: null,
  results: {} as Results,
  lessonCount: 0,
  lessonType: 0,
  settings: {
    count: 0,
    language: null,
    type: -1,
  } as LessonSettings,
};
