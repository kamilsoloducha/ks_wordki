import { LessonStatus, SetLesson } from "pages/lesson/models/lessonState";
import { Repeat } from "pages/lesson/models/repeat";
import Results from "pages/lesson/models/results";
import UserRepeat from "pages/lesson/models/userRepeat";
import { LessonSettings } from "pages/lessonSettings/models/lessonSettings";

export default interface LessonState {
  repeats: Repeat[];
  lessonState: LessonStatus;
  isCorrect: boolean | null;
  answer: string;
  cardsCount: number | null;
  lessonCount: number;
  lessonType: number;

  results: Results;
  settings: LessonSettings;
  lessonHistory: UserRepeat[];
}

export const initialState: LessonState = {
  repeats: [],
  lessonState: SetLesson,
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
  lessonHistory: [],
};
