import { Action } from "@reduxjs/toolkit";
import * as actions from "../reducer";
import UserState from "../state";

const initialState: UserState = {
  expirationDate: "2022/02/02",
  id: "test",
  isLoading: false,
  isLogin: false,
  token: "token",
  errorMessage: "errorMessage",
};

interface Context {
  initialState: UserState;
  action: Action;
  resultState: UserState;
}

export const defaultCtx: Context = {
  initialState: { ...initialState },
  action: { type: "default" },
  resultState: initialState,
};

export const loginCtx: Context = {
  initialState: { ...initialState },
  action: actions.login({ userName: "name", password: "password" }),
  resultState: {
    ...initialState,
    isLoading: true,
  },
};

export const loginSuccessCtx: Context = {
  initialState: { ...initialState },
  action: actions.loginSuccess({ token: "token2", id: "id2", expirationDate: "2022/02/02" }),
  resultState: {
    ...initialState,
    isLogin: true,
    isLoading: false,
    id: "id2",
    token: "token2",
    expirationDate: "2022/02/02",
  },
};

export const registerCtx: Context = {
  initialState: { ...initialState },
  action: actions.register({
    userName: "name",
    email: "email",
    password: "password",
    passwordConfirmation: "password",
  }),
  resultState: {
    ...initialState,
    isLoading: true,
  },
};

export const logoutCtx: Context = {
  initialState: { ...initialState },
  action: actions.logout(),
  resultState: {
    isLogin: false,
    isLoading: false,
    token: "",
    id: "",
    expirationDate: "",
    errorMessage: "",
  },
};

export const setErrorMessageCtx: Context = {
  initialState: { ...initialState },
  action: actions.setErrorMessage("error messages 2"),
  resultState: {
    ...initialState,
    isLoading: false,
    errorMessage: "error messages 2",
  },
};
