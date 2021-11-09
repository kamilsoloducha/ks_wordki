import { LessonAction } from "./actions";
import LessonState, { initialState } from "./state";

export default function lessonReducer(
  state = initialState,
  action: LessonAction
): LessonState {
  return action.type.includes("[LESSON]") ? action.reduce(state) : state;
}
