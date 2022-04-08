import { call, put, take, takeEvery } from "@redux-saga/core/effects";
import * as api from "api";
import { SagaIterator } from "redux-saga";
import { setErrorMessage, UserActionEnum, RegisterUser, login } from "../actions";

export function* registerUserEffect(): SagaIterator {
  const action: RegisterUser = yield take(UserActionEnum.REGISTER);

  const request = {
    userName: action.name,
    password: action.password,
    passwordConfirmation: action.passwordConfirmation,
    email: action.email,
  } as api.RegisterRequest;

  const apiResponse: api.RegisterResponse = yield call(api.register, request);

  switch (apiResponse.responseCode) {
    case api.RegisterResponseCode.Successful:
      yield put(login(action.name, action.password));
      break;
    case api.RegisterResponseCode.UserNameAlreadyOccupied:
      yield put(setErrorMessage("User with the same name has already existed"));
      break;
    case api.RegisterResponseCode.EmailAlreadyOccupied:
      yield put(setErrorMessage("User with the same email has already existed"));
      break;
  }
}
