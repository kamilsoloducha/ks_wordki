import { call, put, take } from "@redux-saga/core/effects";
import { PayloadAction } from "@reduxjs/toolkit";
import * as api from "api";
import { SagaIterator } from "redux-saga";
import { RegisterPayload } from "../action-payload";
import { login, setErrorMessage } from "../reducer";

export function* registerUserEffect(): SagaIterator {
  const action: PayloadAction<RegisterPayload> = yield take("user/register");

  const request = {
    userName: action.payload.userName,
    password: action.payload.password,
    passwordConfirmation: action.payload.passwordConfirmation,
    email: action.payload.email,
  } as api.RegisterRequest;

  const apiResponse: api.RegisterResponse = yield call(api.register, request);

  switch (apiResponse.responseCode) {
    case api.RegisterResponseCode.Successful:
      yield put(login({ userName: action.payload.userName, password: action.payload.password }));
      break;
    case api.RegisterResponseCode.UserNameAlreadyOccupied:
      yield put(setErrorMessage("User with the same name has already existed"));
      break;
    case api.RegisterResponseCode.EmailAlreadyOccupied:
      yield put(setErrorMessage("User with the same email has already existed"));
      break;
  }
}
