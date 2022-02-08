import { MainState } from "store/store";

export const selectRepeats = (state: MainState) => state.lessonReducer.repeats;
export const selectLessonState = (state: MainState) => state.lessonReducer.lessonState;

export const selectCurrectRepeat = (state: MainState) => state.lessonReducer.repeats[0];

export const selectUserAnswer = (state: MainState) => state.lessonReducer.answer;

export const selectLessonHistory = (state: MainState) => state.lessonReducer.lessonHistory;

export const selectIsCorrect = (state: MainState) => state.lessonReducer.isCorrect;

export const selectCardsCount = (state: MainState) => state.lessonReducer.cardsCount;

export const selectShouldSendAnswer = (state: MainState) =>
  state.lessonReducer.lessonCount >= state.lessonReducer.results.answers;

export const selectResults = (state: MainState) => state.lessonReducer.results;
export const selectLessonType = (state: MainState) => state.lessonReducer.lessonType;

export const selectSettings = (state: MainState) => state.lessonReducer.settings;
