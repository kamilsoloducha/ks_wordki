import { LessonStateEnum } from "pages/lesson/models/lessonState";
import { Repeat } from "pages/lesson/models/repeat";
import Results from "pages/lesson/models/results";
import { compare } from "pages/lesson/services/answerComparer";
import {
  calculateResultsForCorrect,
  calculateResultsForWrong,
} from "./helpers/resultsHelpers";
import LessonState, { initialState } from "./state";

export enum DailyActionEnum {
  RESET_ALL = "[LESSON] RESET_ALL",
  RESET_LESSON = "[LESSON] RESET_LESSON",
  RESET_SETTINGS = "[LESSON] RESET_SETTINGS",

  SET_SETTINGS = "[LESSON] SET_SETTINGS",

  GET_CARDS_COUNT = "[LESSON] GET_CARDS_COUNT",
  GET_CARDS_COUNT_SUCCESS = "[LESSON] GET_CARDS_COUNT_SUCCESS",

  GET_CARDS = "[LESSON] GET_CARDS",
  GET_CARDS_SUCCESS = "[LESSON] GET_CARDS_SUCCESS",

  SET_ANSWER = "[LESSON] SET_ANSWER",

  LESSON_START = "[LESSON] LESSON_START",
  LESSON_PAUSE = "[LESSON] LESSON_PAUSE",
  LESSON_CHECK = "[LESSON] LESSON_CHECK",
  LESSON_CORRECT = "[LESSON] LESSON_CORRECT",
  LESSON_WRONG = "[LESSON] LESSON_WRONG",
  LESSON_FINISH = "[LESSON] LESSON_FINISH",
}

export interface LessonAction {
  type: DailyActionEnum;
  reduce: (state: LessonState) => LessonState;
}

export interface Reset extends LessonAction {}
export function resetAll(): Reset {
  return {
    type: DailyActionEnum.RESET_ALL,
    reduce: (_: LessonState): LessonState => {
      return initialState;
    },
  };
}

export interface ResetLesson extends LessonAction {}
export function resetLesson(): ResetLesson {
  return {
    type: DailyActionEnum.RESET_LESSON,
    reduce: (state: LessonState): LessonState => {
      return {
        ...state,
        repeats: [],
        answer: "",
      };
    },
  };
}

export interface SetSettings extends LessonAction {}
export function setSettings(lessonType: number): SetSettings {
  return {
    type: DailyActionEnum.SET_SETTINGS,
    reduce: (state: LessonState): LessonState => {
      return {
        ...state,
        lessonType,
      };
    },
  };
}

export interface ResetSettings extends LessonAction {}
export function resetSettings(): ResetSettings {
  return {
    type: DailyActionEnum.RESET_SETTINGS,
    reduce: (state: LessonState): LessonState => {
      return {
        ...state,
      };
    },
  };
}

export interface GetCards extends LessonAction {
  count: number;
  questionLanguage: number;
}
export function getCards(count: number, questionLanguage: number): GetCards {
  return {
    count,
    questionLanguage,
    type: DailyActionEnum.GET_CARDS,
    reduce: (state: LessonState): LessonState => {
      return { ...state, lessonState: LessonStateEnum.Loading };
    },
  };
}

export interface GetCardsSuccess extends LessonAction {}
export function getCardsSuccess(repeats: Repeat[]): GetCardsSuccess {
  return {
    type: DailyActionEnum.GET_CARDS_SUCCESS,
    reduce: (state: LessonState): LessonState => {
      const allRepeats = state.repeats.concat(repeats);
      return {
        ...state,
        repeats: allRepeats,
        lessonCount: allRepeats.length,
        lessonState: LessonStateEnum.StartLessonPending,
      };
    },
  };
}

export interface GetCardsCount extends LessonAction {
  questionLanguage: number;
}
export function getCardsCount(questionLanguage: number): GetCardsCount {
  return {
    type: DailyActionEnum.GET_CARDS_COUNT,
    reduce: (state: LessonState): LessonState => {
      return { ...state };
    },
    questionLanguage,
  };
}

export interface GetCardsCountSuccess extends LessonAction {}
export function getCardsCountSuccess(count: number): GetCardsCountSuccess {
  return {
    type: DailyActionEnum.GET_CARDS_COUNT_SUCCESS,
    reduce: (state: LessonState): LessonState => {
      return {
        ...state,
        cardsCount: count,
      };
    },
  };
}

export interface StartLesson extends LessonAction {}
export function startLesson(): StartLesson {
  return {
    type: DailyActionEnum.LESSON_START,
    reduce: (state: LessonState): LessonState => {
      const results = {
        answers: 0,
        correct: 0,
        accept: 0,
        wrong: 0,
      } as Results;
      return {
        ...state,
        results: results,
        lessonState: LessonStateEnum.CheckPending,
      };
    },
  };
}

export interface PauseLesson extends LessonAction {}
export function pauseLesson(): PauseLesson {
  return {
    type: DailyActionEnum.LESSON_PAUSE,
    reduce: (state: LessonState): LessonState => {
      return { ...state, lessonState: LessonStateEnum.Pause };
    },
  };
}

export interface Check extends LessonAction {}
export function check(): Check {
  return {
    type: DailyActionEnum.LESSON_CHECK,
    reduce: (state: LessonState): LessonState => {
      const isCorrect =
        state.lessonType === 1
          ? true
          : compare(state.repeats[0].answerValue, state.answer);
      return {
        ...state,
        lessonState: LessonStateEnum.AnswerPending,
        isCorrect: isCorrect,
      };
    },
  };
}

export interface Correct extends LessonAction {
  groupId: string;
  cardId: string;
  side: number;
  result: number;
}
export function correct(
  groupId: string,
  cardId: string,
  side: number,
  result: number
): Correct {
  return {
    groupId,
    cardId,
    side,
    result,
    type: DailyActionEnum.LESSON_CORRECT,
    reduce: (state: LessonState): LessonState => {
      const repeats = state.repeats.slice(1);
      const lessonState =
        repeats.length > 0
          ? LessonStateEnum.CheckPending
          : LessonStateEnum.FinishPending;

      const results = calculateResultsForCorrect(
        state.results,
        state.isCorrect || false,
        state.lessonCount
      );

      return {
        ...state,
        lessonState: lessonState,
        repeats,
        results,
        isCorrect: null,
      };
    },
  };
}

export interface Wrong extends LessonAction {
  groupId: string;
  cardId: string;
  side: number;
  result: number;
}
export function wrong(groupId: string, cardId: string, side: number): Wrong {
  return {
    groupId,
    cardId,
    side,
    result: -1,
    type: DailyActionEnum.LESSON_WRONG,
    reduce: (state: LessonState): LessonState => {
      const currentRepeat = state.repeats[0];
      const repeats = state.repeats.slice(1);
      repeats.push(currentRepeat);

      const results = calculateResultsForWrong(
        state.results,
        state.lessonCount
      );

      return {
        ...state,
        lessonState: LessonStateEnum.CheckPending,
        repeats,
        results,
        isCorrect: null,
      };
    },
  };
}

export interface FinishLesson extends LessonAction {}
export function finishLesson(): FinishLesson {
  return {
    type: DailyActionEnum.LESSON_FINISH,
    reduce: (state: LessonState): LessonState => {
      return { ...state, lessonState: LessonStateEnum.FinishPending };
    },
  };
}

export interface SetAnswer extends LessonAction {}
export function setAnswer(answer: string): SetAnswer {
  return {
    type: DailyActionEnum.SET_ANSWER,
    reduce: (state: LessonState): LessonState => {
      return { ...state, answer };
    },
  };
}
