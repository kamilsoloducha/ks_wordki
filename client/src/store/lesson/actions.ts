import { LessonStateEnum } from "pages/lesson/models/lessonState";
import { Repeat } from "pages/lesson/models/repeat";
import LessonState, { initialState } from "./state";

export enum DailyActionEnum {
  RESET = "[LESSON] RESET",

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
export function reset(): Reset {
  return {
    type: DailyActionEnum.RESET,
    reduce: (_: LessonState): LessonState => {
      return initialState;
    },
  };
}

export interface GetCards extends LessonAction {
  count: number;
}
export function getCards(count: number): GetCards {
  return {
    count,
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
        lessonState: LessonStateEnum.StartLessonPending,
      };
    },
  };
}

export interface StartLesson extends LessonAction {}
export function startLesson(): StartLesson {
  return {
    type: DailyActionEnum.LESSON_START,
    reduce: (state: LessonState): LessonState => {
      return { ...state, lessonState: LessonStateEnum.CheckPending };
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
      const isCorrect = state.repeats[0].answerValue === state.answer;
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
      return {
        ...state,
        lessonState: LessonStateEnum.CheckPending,
        repeats,
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
      return {
        ...state,
        lessonState: LessonStateEnum.CheckPending,
        repeats,
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
