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

  SET_SETTING_LANGUAGE = "[LESSON] SET_SETTING_LANGUAGE",
  SET_SETTING_COUNT = "[LESSON] SET_SETTING_COUNT",
  SET_SETTING_TYPE = "[LESSON] SET_SETTING_TYPE",

  GET_CARDS_COUNT = "[LESSON] GET_CARDS_COUNT",
  GET_CARDS_COUNT_SUCCESS = "[LESSON] GET_CARDS_COUNT_SUCCESS",

  GET_CARDS = "[LESSON] GET_CARDS",
  GET_CARDS_SUCCESS = "[LESSON] GET_CARDS_SUCCESS",

  TICK_CARD = "[LESSON] TICK_CARD",

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

export interface SetSettingCount extends LessonAction {}
export function setSettingCount(count: number): SetSettingCount {
  return {
    type: DailyActionEnum.SET_SETTING_COUNT,
    reduce: (state: LessonState): LessonState => {
      return {
        ...state,
        settings: {
          ...state.settings,
          count: count,
        },
      };
    },
  };
}

export interface SetSettingLangauge extends LessonAction {}
export function setSettingLanguage(language: number): SetSettingCount {
  return {
    type: DailyActionEnum.SET_SETTING_LANGUAGE,
    reduce: (state: LessonState): LessonState => {
      return {
        ...state,
        settings: {
          ...state.settings,
          language: language,
        },
      };
    },
  };
}

export interface SetSettingType extends LessonAction {}
export function setSettingType(type: number): SetSettingCount {
  return {
    type: DailyActionEnum.SET_SETTING_TYPE,
    reduce: (state: LessonState): LessonState => {
      return {
        ...state,
        lessonType: type,
        settings: {
          ...state.settings,
          language: type,
        },
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

export interface GetCards extends LessonAction {}
export function getCards(): GetCards {
  return {
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

export interface GetCardsCount extends LessonAction {}
export function getCardsCount(): GetCardsCount {
  return {
    type: DailyActionEnum.GET_CARDS_COUNT,
    reduce: (state: LessonState): LessonState => {
      return { ...state };
    },
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

export interface TickCard extends LessonAction {
  sideId: number;
}
export function tickCard(sideId: number): TickCard {
  return {
    type: DailyActionEnum.TICK_CARD,
    reduce: (state: LessonState): LessonState => {
      return {
        ...state,
      };
    },
    sideId,
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
          : compare(state.repeats[0].answer, state.answer);
      return {
        ...state,
        lessonState: LessonStateEnum.AnswerPending,
        isCorrect: isCorrect,
      };
    },
  };
}

export interface Correct extends LessonAction {
  sideId: number;
  result: number;
}
export function correct(sideId: number, result: number): Correct {
  return {
    sideId,
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
  sideId: number;
  result: number;
}
export function wrong(sideId: number): Wrong {
  return {
    sideId,
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
