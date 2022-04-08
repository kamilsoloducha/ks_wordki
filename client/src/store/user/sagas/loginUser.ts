import { call, put, take } from "@redux-saga/core/effects";
import * as api from "api";
import { SagaIterator } from "redux-saga";
import { setErrorMessage, loginSuccess, LoginUser, UserActionEnum } from "../actions";

export function* loginUserEffect(): SagaIterator {
  const action: LoginUser = yield take(UserActionEnum.LOGIN);

  const request = {
    userName: action.name,
    password: action.password,
  } as api.LoginRequest;
  const apiResponse: api.LoginResponse = yield call(api.login, request);

  switch (apiResponse.responseCode) {
    case api.LoginResponseCode.Successful:
      localStorage.setItem("id", apiResponse.id);
      localStorage.setItem("token", apiResponse.token);
      localStorage.setItem("creationDate", apiResponse.creatingDateTime);
      localStorage.setItem("expirationDate", apiResponse.expirationDateTime);
      yield put(
        loginSuccess(apiResponse.token, apiResponse.id, new Date(apiResponse.expirationDateTime))
      );
      break;
    case api.LoginResponseCode.UserNotFound:
      yield put(setErrorMessage("Incorrect username or password."));
      break;
  }
}
