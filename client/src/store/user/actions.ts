import { Action } from "redux";
import UserState, { initialState } from "./state";

export enum UserActionEnum {
  LOGIN = "[USER] LOGIN",
  LOGIN_SUCCESS = "[USER] LOGIN_SUCCESS",
  REGISTER = "[USER] REGISTER",
  REGISTER_SUCCESS = "[USER] REGISTER_SUCCESS",
  LOGOUT = "[USER] LOGOUT",

  SET_ERROR_MESSAGE = "[USER] SET_ERROR_MESSAGE",
}

export interface LoginUser extends Action {
  name: string;
  password: string;
}
export function getLoginUser(name: string, password: string): LoginUser {
  return {
    name,
    password,
    type: UserActionEnum.LOGIN,
  };
}
export function reduceLoginUser(state: UserState, _: LoginUser): UserState {
  return { ...state, isLoading: true };
}

export interface LoginUserSuccess extends Action {
  token: string;
  id: string;
  expirationDate: Date;
}
export function getLoginUserSuccess(token: string, id: string, expirationDate: Date) {
  return {
    type: UserActionEnum.LOGIN_SUCCESS,
    token,
    id,
    expirationDate,
  };
}

export function reduceLoginUserSuccess(state: UserState, action: LoginUserSuccess): UserState {
  return {
    ...state,
    id: action.id,
    token: action.token,
    expirationDate: action.expirationDate,
    isLoading: false,
    isLogin: true,
  };
}

export interface SetErrorMessage extends Action {
  errorMessage: string;
}
export function setErrorMessage(errorMessage: string): SetErrorMessage {
  return {
    type: UserActionEnum.SET_ERROR_MESSAGE,
    errorMessage,
  };
}
export function reduceSetErrorMessage(state: UserState, action: SetErrorMessage): UserState {
  return {
    ...state,
    isLoading: false,
    errorMessage: action.errorMessage,
  };
}

export interface Logout extends Action {}

export function getLogoutUser(): Logout {
  return {
    type: UserActionEnum.LOGOUT,
  };
}
export function reduce(_: UserState): UserState {
  return initialState;
}

export interface RegisterUser extends Action {
  name: string;
  email: string;
  password: string;
  passwordConfirmation: string;
}

export function getRegisterUser(
  name: string,
  email: string,
  password: string,
  passwordConfirmation: string
): RegisterUser {
  return {
    name,
    email,
    password,
    passwordConfirmation,
    type: UserActionEnum.REGISTER,
  };
}
export function reduceRegisterUser(state: UserState, _: RegisterUser): UserState {
  return { ...state, isLoading: true };
}
