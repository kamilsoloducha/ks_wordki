import * as actions from "../actions";
import LessonState, { initialState } from "../state";

interface Context {
  givenState: LessonState;
  givenAction: actions.LessonAction;
  expectedState: LessonState;
}

export class ResetLessonCtx implements Context {
  givenState = { ...initialState };
  givenAction = actions.resetAll();
  expectedState = initialState;
}
