import UserState, { initialState } from "./state";

export enum UserActionEnum {
  LOGIN = "[USER] LOGIN",
  LOGIN_SUCCESS = "[USER] LOGIN_SUCCESS",
  REGISTER = "[USER] REGISTER",
  REGISTER_SUCCESS = "[USER] REGISTER_SUCCESS",
  LOGOUT = "[USER] LOGOUT",
}

export interface UserAction {
  type: UserActionEnum;
  reduce: (state: UserState) => UserState;
}

export interface LoginUser extends UserAction {
  name: string;
  password: string;
}

export function getLoginUser(name: string, password: string): LoginUser {
  return {
    name,
    password,
    type: UserActionEnum.LOGIN,
    reduce: (state: UserState): UserState => {
      return { ...state, isLoading: true };
    },
  };
}

export interface LoginUserSuccess extends UserAction {}

export function getLoginUserSuccess(
  token: string,
  id: string,
  expirationDate: Date
): LoginUserSuccess {
  return {
    type: UserActionEnum.LOGIN_SUCCESS,
    reduce: (state: UserState): UserState => {
      return {
        ...state,
        id,
        token,
        expirationDate,
        isLoading: false,
        isLogin: true,
      };
    },
  };
}

export interface Logout extends UserAction {}

export function getLogoutUser(): Logout {
  return {
    type: UserActionEnum.LOGOUT,
    reduce: (_: UserState): UserState => {
      return initialState;
    },
  };
}

export interface RegisterUser extends UserAction {
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
    reduce: (state: UserState): UserState => {
      return state;
    },
  };
}
