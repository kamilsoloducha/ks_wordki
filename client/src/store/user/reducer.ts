import { Action } from "@reduxjs/toolkit";
import * as actions from "./actions";
import UserState, { initialState } from "./state";

export default function userReducer(state = initialState, action: Action): UserState {
  switch (action.type) {
    case actions.UserActionEnum.LOGIN:
      return actions.reduceLoginUser(state, action as actions.LoginUser);
    case actions.UserActionEnum.LOGIN_SUCCESS:
      return actions.reduceLoginUserSuccess(state, action as actions.LoginUserSuccess);
    case actions.UserActionEnum.SET_ERROR_MESSAGE:
      return actions.reduceSetErrorMessage(state, action as actions.SetErrorMessage);
    case actions.UserActionEnum.REGISTER:
      return actions.reduceRegisterUser(state, action as actions.RegisterUser);
    case actions.UserActionEnum.LOGOUT:
      return actions.reduce(state);
    default:
      return state;
  }
}
