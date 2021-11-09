import { UserAction, UserActionEnum } from "./actions";
import UserState, { initialState } from "./state";

export default function userReducer(
  state = initialState,
  action: UserAction
): UserState {
  switch (action.type) {
    case UserActionEnum.LOGIN:
      return action.reduce(state);
    case UserActionEnum.LOGIN_SUCCESS:
      return action.reduce(state);
    case UserActionEnum.REGISTER:
      return action.reduce(state);
    case UserActionEnum.REGISTER_SUCCESS:
      return action.reduce(state);
    case UserActionEnum.LOGOUT:
      return action.reduce(state);
    default:
      return state;
  }
}
