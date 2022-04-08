import { Action } from "@reduxjs/toolkit";
import { FormModel } from "common/components/dialogs/cardDialog/CardForm";
import { Side } from "common/models/side";
import * as l from "pages/lesson/models/lessonState";
import { Repeat } from "pages/lesson/models/repeat";
import Results from "pages/lesson/models/results";
import UserRepeat from "pages/lesson/models/userRepeat";
import { compare } from "pages/lesson/services/compare";
import { Group } from "pages/lessonSettings/models/group";
import { calculateResultsForCorrect, calculateResultsForWrong } from "./helpers/resultsHelpers";
import LessonState, { initialState } from "./state";

export enum DailyActionEnum {
  RESET_ALL = "[LESSON] RESET_ALL",
  RESET_LESSON = "[LESSON] RESET_LESSON",
  RESET_SETTINGS = "[LESSON] RESET_SETTINGS",

  SET_SETTING_LANGUAGE = "[LESSON] SET_SETTING_LANGUAGE",
  SET_SETTING_COUNT = "[LESSON] SET_SETTING_COUNT",
  SET_SETTING_TYPE = "[LESSON] SET_SETTING_TYPE",
  SET_SETTING_MODE = "[LESSON] SET_SETTING_MODE",
  SET_SETTING_GROUP = "[LESSON] SET_SETTING_GROUP",
  SET_SETTING_LIMIT = "[LESSON] SET_SETTING_LIMIT",

  GET_CARDS_COUNT = "[LESSON] GET_CARDS_COUNT",
  GET_CARDS_COUNT_SUCCESS = "[LESSON] GET_CARDS_COUNT_SUCCESS",

  GET_CARDS = "[LESSON] GET_CARDS",
  GET_CARDS_SUCCESS = "[LESSON] GET_CARDS_SUCCESS",

  GET_GROUPS = "[LESSON] GET_GROUPS",
  GET_GROUPS_SUCCESS = "[LESSON] GET_GROUPS_SUCCESS",

  TICK_CARD = "[LESSON] TICK_CARD",

  SET_ANSWER = "[LESSON] SET_ANSWER",

  LESSON_START = "[LESSON] LESSON_START",
  LESSON_PAUSE = "[LESSON] LESSON_PAUSE",
  LESSON_CHECK = "[LESSON] LESSON_CHECK",
  LESSON_CORRECT = "[LESSON] LESSON_CORRECT",
  LESSON_WRONG = "[LESSON] LESSON_WRONG",
  LESSON_NO_MORE_CARDS = "[LESSON] LESSON_NO_MORE_CARDS",
  LESSON_FINISH = "[LESSON] LESSON_FINISH",

  RESET_RESULTS = "[LESSON] RESET_RESULTS",

  UPDATE_CARD = "[LESSON] UPDATE_CARD",
  UPDATE_CARD_SUCCESS = "[LESSON] UPDATE_CARD_SUCCESS",

  DELETE_CARD = "[LESSON] DELETE_CARD",
  DELETE_CARD_SUCCESS = "[LESSON] DELETE_CARD_SUCCESS",
}

export interface LessonAction extends Action {
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
export function setSettingLanguage(languages: number[]): SetSettingCount {
  return {
    type: DailyActionEnum.SET_SETTING_LANGUAGE,
    reduce: (state: LessonState): LessonState => {
      return {
        ...state,
        settings: {
          ...state.settings,
          languages: languages,
        },
      };
    },
  };
}

export interface SetSettingType extends LessonAction {}
export function setSettingType(type: number): SetSettingType {
  return {
    type: DailyActionEnum.SET_SETTING_TYPE,
    reduce: (state: LessonState): LessonState => {
      return {
        ...state,
        lessonType: type,
        settings: {
          ...state.settings,
          type: type,
        },
      };
    },
  };
}

export interface SetSettingMode extends LessonAction {}
export function setSettingMode(mode: number): SetSettingMode {
  return {
    type: DailyActionEnum.SET_SETTING_MODE,
    reduce: (state: LessonState): LessonState => {
      return {
        ...state,
        settings: {
          ...state.settings,
          mode: mode,
        },
      };
    },
  };
}

export function setSettingGroup(groupId: string): LessonAction {
  return {
    type: DailyActionEnum.SET_SETTING_GROUP,
    reduce: (state: LessonState): LessonState => {
      return {
        ...state,
        settings: {
          ...state.settings,
          selectedGroupId: groupId,
        },
      };
    },
  };
}

export function setSettingWrongLimit(limit: number): LessonAction {
  return {
    type: DailyActionEnum.SET_SETTING_LIMIT,
    reduce: (state: LessonState): LessonState => {
      return {
        ...state,
        settings: {
          ...state.settings,
          wrongLimit: limit,
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

export function getCards(): LessonAction {
  return {
    type: DailyActionEnum.GET_CARDS,
    reduce: (state: LessonState): LessonState => {
      return {
        ...state,
        lessonState: l.Loading,
        isProcessing: true,
        lessonHistory: [],
      };
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
        lessonState: l.StartLessonPending,
        isProcessing: false,
      };
    },
  };
}

export function getGroups(): LessonAction {
  return {
    type: DailyActionEnum.GET_GROUPS,
    reduce: (state: LessonState): LessonState => {
      return { ...state };
    },
  };
}

export function getGroupsSuccess(groups: Group[]): LessonAction {
  return {
    type: DailyActionEnum.GET_GROUPS_SUCCESS,
    reduce: (state: LessonState): LessonState => {
      return {
        ...state,
        settings: {
          ...state.settings,
          groups: groups,
        },
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
      const settingsCount = state.settings.count > count ? count : state.settings.count;
      return {
        ...state,
        cardsCount: count,
        settings: {
          ...state.settings,
          count: settingsCount,
        },
      };
    },
  };
}

export interface TickCard extends LessonAction {}
export function tickCard(): TickCard {
  return {
    type: DailyActionEnum.TICK_CARD,
    reduce: (state: LessonState): LessonState => {
      return {
        ...state,
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
        lessonState: l.CheckPending,
      };
    },
  };
}

export interface PauseLesson extends LessonAction {}
export function pauseLesson(): PauseLesson {
  return {
    type: DailyActionEnum.LESSON_PAUSE,
    reduce: (state: LessonState): LessonState => {
      return { ...state, lessonState: l.Pause };
    },
  };
}

export interface Check extends LessonAction {}
export function check(): Check {
  return {
    type: DailyActionEnum.LESSON_CHECK,
    reduce: (state: LessonState): LessonState => {
      const isCorrect = state.lessonType === 1 || compare(state.repeats[0].answer, state.answer);
      if (!isCorrect && !state.isSecondChangeUsed && state.repeats[0].questionDrawer > 4) {
        return {
          ...state,
          isSecondChangeUsed: true,
          answer: "",
        };
      }
      return {
        ...state,
        lessonState: l.AnswerPending,
        isSecondChangeUsed: false,
        isCorrect: isCorrect,
      };
    },
  };
}

export interface Correct extends LessonAction {
  result: number;
}
export function correct(result: number): Correct {
  return {
    result,
    type: DailyActionEnum.LESSON_CORRECT,
    reduce: (state: LessonState): LessonState => {
      const lessonHistory = [
        ...state.lessonHistory,
        { repeat: state.repeats[0], userAnswer: state.answer, result: result } as UserRepeat,
      ];

      const repeats = state.repeats.slice(1);
      const lessonState = repeats.length > 0 ? l.CheckPending : l.FinishPending;

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
        lessonHistory: lessonHistory,
      };
    },
  };
}

export interface Wrong extends LessonAction {
  result: number;
}
export function wrong(): Wrong {
  return {
    result: -1,
    type: DailyActionEnum.LESSON_WRONG,
    reduce: (state: LessonState): LessonState => {
      const lessonHistory = [
        ...state.lessonHistory,
        { repeat: state.repeats[0], userAnswer: state.answer, result: -1 } as UserRepeat,
      ];
      const currentRepeat = state.repeats[0];
      const repeats = state.repeats.slice(1);
      repeats.push(currentRepeat);

      const results = calculateResultsForWrong(state.results, state.lessonCount);

      return {
        ...state,
        lessonState: l.CheckPending,
        repeats,
        results,
        isCorrect: null,
        lessonHistory,
      };
    },
  };
}

export function noMoreCards(): LessonAction {
  return {
    type: DailyActionEnum.LESSON_FINISH,
    reduce: (state: LessonState): LessonState => {
      return { ...state, lessonState: l.FinishPending };
    },
  };
}

export function finishLesson(): LessonAction {
  return {
    type: DailyActionEnum.LESSON_FINISH,
    reduce: (state: LessonState): LessonState => {
      return { ...state, lessonState: l.FinishPending };
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

export interface ResetResults extends Action {}
export function resetResults(): ResetResults {
  const action: ResetResults = {
    type: DailyActionEnum.RESET_RESULTS,
  };
  return action;
}
export function resetResultsReduce(state: LessonState): LessonState {
  const newResults = initialState.results;
  return { ...state, results: newResults };
}

export interface UpdateCard extends Action {
  form: FormModel;
  groupId: string;
}
export function updateCard(form: FormModel, groupId: string): UpdateCard {
  const action: UpdateCard = {
    type: DailyActionEnum.UPDATE_CARD,
    form,
    groupId,
  };
  return action;
}
export function updateCardReduce(state: LessonState): LessonState {
  return { ...state };
}

export interface UpdateCardSuccess extends Action {
  form: FormModel;
}
export function updateCardSuccess(form: FormModel): UpdateCardSuccess {
  const action: UpdateCardSuccess = {
    type: DailyActionEnum.UPDATE_CARD_SUCCESS,
    form,
  };
  return action;
}
export function updateCardSuccessReduce(
  state: LessonState,
  action: UpdateCardSuccess
): LessonState {
  const lessonHistory = state.lessonHistory.map((x) => {
    if (x.repeat.cardId !== action.form.cardId) return x;
    x.repeat.question =
      x.repeat.questionSide === Side.Front ? action.form.frontValue : action.form.backValue;
    x.repeat.questionExample =
      x.repeat.questionSide === Side.Front ? action.form.frontExample : action.form.backExample;
    x.repeat.answer =
      x.repeat.questionSide === Side.Front ? action.form.backValue : action.form.frontValue;
    x.repeat.answerExample =
      x.repeat.questionSide === Side.Front ? action.form.backExample : action.form.backExample;
    return x;
  });

  return { ...state, lessonHistory: lessonHistory };
}
