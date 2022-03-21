import { call, put, takeEvery } from "@redux-saga/core/effects";
import * as api from "pages/register/services/registerApi";
import RegisterRequest from "pages/register/models/registerRequest";
import { setErrorMessage, UserActionEnum, RegisterUser, getLoginUser } from "../actions";
import { RegisterResponse, RegisterResponseCode } from "pages/register/models/registerResponse";

export function* registerUser(action: RegisterUser) {
  const request = {
    userName: action.name,
    password: action.password,
    passwordConfirmation: action.passwordConfirmation,
    email: action.email,
  } as RegisterRequest;

  const apiResponse: RegisterResponse = yield call(api.register, request);

  switch (apiResponse.responseCode) {
    case RegisterResponseCode.Successful:
      yield put(getLoginUser(action.name, action.password));
      break;
    case RegisterResponseCode.UserNameAlreadyOccupied:
      yield put(setErrorMessage("User with the same name has already existed"));
      break;
    case RegisterResponseCode.EmailAlreadyOccupied:
      yield put(setErrorMessage("User with the same email has already existed"));
      break;
  }
}

export function* registerUserEffect() {
  yield takeEvery(UserActionEnum.REGISTER, registerUser);
}
