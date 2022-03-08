import { DailyActionEnum, LessonAction, resetResultsReduce } from "./actions";
import LessonState, { initialState } from "./state";

export default function lessonReducer(state = initialState, action: LessonAction): LessonState {
  switch (action.type) {
    case DailyActionEnum.RESET_RESULTS:
      return resetResultsReduce(state);
  }
  return action.type.includes("[LESSON]") ? action.reduce(state) : state;
}
