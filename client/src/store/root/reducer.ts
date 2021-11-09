import RootState, { initialState } from "./state";
import { RootAction, RootActionEnum } from "./actions";

export default function rootReducer(
  state = initialState,
  action: RootAction
): RootState {
  switch (action.type) {
    case RootActionEnum.REQUEST_FAILED:
      return action.reduce(state);
    default:
      return state;
  }
}
