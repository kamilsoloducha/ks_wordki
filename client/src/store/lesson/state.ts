import { LessonStatus, SetLesson } from "pages/lesson/models/lessonState";
import { Repeat } from "pages/lesson/models/repeat";
import Results from "pages/lesson/models/results";
import UserRepeat from "pages/lesson/models/userRepeat";
import { Language } from "pages/lessonSettings/models/languages";
import { LessonSettings } from "pages/lessonSettings/models/lessonSettings";

export default interface LessonState {
  isProcessing: boolean;
  repeats: Repeat[];
  lessonState: LessonStatus;
  isCorrect: boolean | null;
  isSecondChangeUsed: boolean;
  answer: string;
  cardsCount: number | null;
  lessonCount: number;
  lessonType: number;

  results: Results;
  settings: LessonSettings;
  lessonHistory: UserRepeat[];

  languages: Language[];
}

export const initialState: LessonState = {
  isProcessing: false,
  repeats: [],
  lessonState: SetLesson,
  isCorrect: null,
  isSecondChangeUsed: false,
  answer: "",
  cardsCount: null,
  results: {} as Results,
  lessonCount: 0,
  lessonType: 0,
  settings: {
    mode: 1,
    count: 0,
    languages: [],
    type: -1,
    groups: [],
    selectedGroupId: null,
    wrongLimit: 15,
  } as LessonSettings,
  lessonHistory: [],
  languages: [],
};
