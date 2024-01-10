import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import LessonState, { initialState } from "./state";
import * as p from "./action-payloads";
import * as lessonState from "pages/lesson/models/lessonState";
import Results from "pages/lesson/models/results";
import { compare } from "pages/lesson/services/compare";
import UserRepeat from "pages/lesson/models/userRepeat";
import { calculateResultsForCorrect, calculateResultsForWrong } from "./helpers/resultsHelpers";

export const lessonSlice = createSlice({
  name: "lesson",
  initialState,
  reducers: {
    resetAll: (state: LessonState): void => {
      state.answer = initialState.answer;
      state.cardsCount = initialState.cardsCount;
      state.isCorrect = initialState.isCorrect;
      state.isProcessing = initialState.isProcessing;
      state.isSecondChangeUsed = initialState.isSecondChangeUsed;
      state.lessonCount = initialState.lessonCount;
      state.lessonHistory = initialState.lessonHistory;
      state.lessonType = initialState.lessonType;
      state.repeats = initialState.repeats;
      state.settings = initialState.settings;
    },
    resetLesson: (state: LessonState): void => {
      state.repeats = [];
      state.answer = "";
    },
    resetSettings: (_: LessonState): void => undefined,
    setSettingsCount: (state: LessonState, action: PayloadAction<p.SetSettingCount>): void => {
      state.settings.count = action.payload.count;
    },
    setSettingsLanguage: (
      state: LessonState,
      action: PayloadAction<p.SetSettingLanguage>
    ): void => {
      state.settings.languages = action.payload.languages;
    },
    setSettingsType: (state: LessonState, action: PayloadAction<p.SetSettingType>): void => {
      state.lessonType = action.payload.type;
      state.settings.type = action.payload.type;
    },
    setSettingsMode: (state: LessonState, action: PayloadAction<p.SetSettingMode>): void => {
      state.settings.mode = action.payload.mode;
    },
    setSettingsGroup: (state: LessonState, action: PayloadAction<p.SetSettingGroup>): void => {
      state.settings.selectedGroupId = action.payload.groupId;
    },
    getLanguages: (_: LessonState): void => undefined,
    getLanguagesSuccess: (
      state: LessonState,
      action: PayloadAction<p.GetLanguagesSuccess>
    ): void => {
      state.languages = action.payload.languages;
    },
    getCardsCount: (_: LessonState): void => undefined,
    getCardsCountSuccess: (
      state: LessonState,
      action: PayloadAction<p.GetCardsCountSuccess>
    ): void => {
      state.cardsCount = action.payload.count;
      state.settings.count = action.payload.count;
    },
    getCards: (state: LessonState): void => {
      state.lessonState = lessonState.Loading;
      state.isProcessing = true;
      state.lessonHistory = [];
    },
    getCardsSuccess: (state: LessonState, action: PayloadAction<p.GetCardsSuccess>): void => {
      const allRepeats = action.payload.repeats.concat(state.repeats);
      state.lessonState = lessonState.StartLessonPending;
      state.isProcessing = false;
      state.lessonCount = allRepeats.length;
      state.repeats = allRepeats;
    },
    getGroups: (_: LessonState): void => undefined,
    getGroupsSuccess: (state: LessonState, action: PayloadAction<p.GetGroupsSuccess>): void => {
      state.settings.groups = action.payload.groups;
    },
    tickCard: (_: LessonState): void => undefined,
    startLesson: (state: LessonState): void => {
      const results = {
        answers: 0,
        correct: 0,
        accept: 0,
        wrong: 0,
      } as Results;
      state.results = results;
      state.lessonState = lessonState.CheckPending;
    },
    check: (state: LessonState): void => {
      const isCorrect = state.lessonType === 1 || compare(state.repeats[0].answer, state.answer);
      if (!isCorrect && !state.isSecondChangeUsed && state.repeats[0].questionDrawer > 4) {
        state.answer = "";
        state.isSecondChangeUsed = true;
      } else {
        state.lessonState = lessonState.AnswerPending;
        state.isSecondChangeUsed = false;
        state.isCorrect = isCorrect;
      }
    },
    correct: (state: LessonState, action: PayloadAction<p.Correct>): void => {
      state.lessonHistory.push({
        repeat: state.repeats[0],
        userAnswer: state.answer,
        result: action.payload.result,
      } as UserRepeat);
      const repeats = state.repeats.slice(1);

      const results = calculateResultsForCorrect(
        state.results,
        state.isCorrect || false,
        state.lessonCount
      );
      state.lessonState = repeats.length > 0 ? lessonState.CheckPending : lessonState.FinishPending;
      state.repeats = repeats;
      state.results = results;
      state.isCorrect = null;
    },
    wrong: (state: LessonState, _: PayloadAction<p.Wrong>): void => {
      state.lessonHistory.push({
        repeat: state.repeats[0],
        userAnswer: state.answer,
        result: -1,
      } as UserRepeat);

      const currentRepeat = state.repeats[0];
      const repeats = state.repeats.slice(1);
      repeats.push(currentRepeat);

      const results = calculateResultsForWrong(state.results, state.lessonCount);

      state.lessonState = lessonState.CheckPending;
      state.repeats = repeats;
      state.results = results;
      state.isCorrect = null;
    },
    noMoreCards: (state: LessonState): void => {
      state.lessonState = lessonState.FinishPending;
    },
    finishLesson: (state: LessonState): void => {
      state.lessonState = lessonState.FinishPending;
    },
    setAnswer: (state: LessonState, action: PayloadAction<p.SetAnswer>): void => {
      state.answer = action.payload.answer;
    },
    resetResults: (state: LessonState): void => {
      state.results = initialState.results;
    },
    updateCard: (_: LessonState, __: PayloadAction<p.UpdateCard>): void => undefined,
    updateCardSuccess: (state: LessonState, action: PayloadAction<p.UpdateCardSuccess>): void => {
      const lessonHistory = state.lessonHistory.map((x) => {
        if (x.repeat.cardId !== action.payload.form.cardId) return x;
        return x;
      });

      state.lessonHistory = lessonHistory;
    },
  },
});

export default lessonSlice.reducer;

export const {
  check,
  correct,
  finishLesson,
  getLanguages,
  getLanguagesSuccess,
  getCards,
  getCardsCount,
  getCardsCountSuccess,
  getCardsSuccess,
  getGroups,
  getGroupsSuccess,
  noMoreCards,
  resetAll,
  resetLesson,
  resetResults,
  resetSettings,
  setAnswer,
  setSettingsCount,
  setSettingsGroup,
  setSettingsLanguage,
  setSettingsMode,
  setSettingsType,
  startLesson,
  tickCard,
  updateCard,
  updateCardSuccess,
  wrong,
} = lessonSlice.actions;
