import { MainState } from "store/store";

export const selectRepeats = (state: MainState) => state.lessonReducer.repeats;
export const selectLessonState = (state: MainState) =>
  state.lessonReducer.lessonState;

export const selectCurrectRepeat = (state: MainState) =>
  state.lessonReducer.repeats[0];

export const selectIsCorrect = (state: MainState) =>
  state.lessonReducer.isCorrect;
