import { call, put, takeEvery } from "@redux-saga/core/effects";
import { LoginRequest } from "api/commands";
import * as api from "api";
import { setErrorMessage, getLoginUserSuccess, LoginUser, UserActionEnum } from "../actions";

export function* loginUser(action: LoginUser) {
  const request = {
    userName: action.name,
    password: action.password,
  } as LoginRequest;
  const apiResponse: api.LoginResponse = yield call(api.login, request);

  switch (apiResponse.responseCode) {
    case api.LoginResponseCode.Successful:
      localStorage.setItem("id", apiResponse.id);
      localStorage.setItem("token", apiResponse.token);
      localStorage.setItem("creationDate", apiResponse.creatingDateTime);
      localStorage.setItem("expirationDate", apiResponse.expirationDateTime);
      yield put(
        getLoginUserSuccess(
          apiResponse.token,
          apiResponse.id,
          new Date(apiResponse.expirationDateTime)
        )
      );
      break;
    case api.LoginResponseCode.UserNotFound:
      yield put(setErrorMessage("Incorrect username or password."));
      break;
  }
}

export function* loginUserEffect() {
  yield takeEvery(UserActionEnum.LOGIN, loginUser);
}
