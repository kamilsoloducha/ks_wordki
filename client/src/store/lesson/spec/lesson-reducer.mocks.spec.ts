import { Action } from "@reduxjs/toolkit";
import * as actions from "../reducer";
import LessonState, { initialState } from "../state";

interface Context {
  givenState: LessonState;
  givenAction: Action;
  expectedState: LessonState;
}

export class ResetLessonCtx implements Context {
  givenState = { ...initialState };
  givenAction = actions.resetAll();
  expectedState = initialState;
}
