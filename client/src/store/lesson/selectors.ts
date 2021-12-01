import { MainState } from "store/store";

export const selectRepeats = (state: MainState) => state.lessonReducer.repeats;
export const selectLessonState = (state: MainState) =>
  state.lessonReducer.lessonState;

export const selectCurrectRepeat = (state: MainState) =>
  state.lessonReducer.repeats[0];

export const selectIsCorrect = (state: MainState) =>
  state.lessonReducer.isCorrect;

export const selectCardsCount = (state: MainState) =>
  state.lessonReducer.cardsCount;

export const selectShouldSendAnswer = (state: MainState) =>
  state.lessonReducer.lessonCount >= state.lessonReducer.results.answers;

export const selectResults = (state: MainState) => state.lessonReducer.results;
