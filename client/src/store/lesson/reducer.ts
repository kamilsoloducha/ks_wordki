import * as actions from "./actions";
import LessonState, { initialState } from "./state";

export default function lessonReducer(
  state = initialState,
  action: actions.LessonAction
): LessonState {
  switch (action.type) {
    case actions.DailyActionEnum.RESET_RESULTS:
      return actions.resetResultsReduce(state);
    case actions.DailyActionEnum.UPDATE_CARD:
      return actions.updateCardReduce(state);
    case actions.DailyActionEnum.UPDATE_CARD_SUCCESS:
      return actions.updateCardSuccessReduce(state, action as any);
  }
  return action.type.includes("[LESSON]") ? action.reduce(state) : state;
}
