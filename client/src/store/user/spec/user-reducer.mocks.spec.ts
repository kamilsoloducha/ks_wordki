import * as actions from "../actions";
import { Action } from "@reduxjs/toolkit";
import UserState from "../state";

const initialState: UserState = {
  expirationDate: new Date(),
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
  action: actions.login("name", "password"),
  resultState: {
    ...initialState,
    isLoading: true,
  },
};

export const loginSuccessCtx: Context = {
  initialState: { ...initialState },
  action: actions.loginSuccess("token2", "id2", new Date(2)),
  resultState: {
    ...initialState,
    isLogin: true,
    isLoading: false,
    id: "id2",
    token: "token2",
    expirationDate: new Date(2),
  },
};

export const registerCtx: Context = {
  initialState: { ...initialState },
  action: actions.register("name", "email", "password", "password"),
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
    expirationDate: new Date(1),
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
