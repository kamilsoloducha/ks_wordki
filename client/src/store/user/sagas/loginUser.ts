import { call, put, takeEvery } from "@redux-saga/core/effects";
import { LoginRequest, LoginResponse } from "pages/login/requests";
import { LoginResponseCode } from "pages/login/requests/responses/loginResponseCode";
import { login } from "pages/login/services/loginApi";
import { setErrorMessage, getLoginUserSuccess, LoginUser, UserActionEnum } from "../actions";

export function* loginUser(action: LoginUser) {
  const request = {
    userName: action.name,
    password: action.password,
  } as LoginRequest;
  const apiResponse: LoginResponse = yield call(login, request);

  switch (apiResponse.responseCode) {
    case LoginResponseCode.Successful:
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
    case LoginResponseCode.UserNotFound:
      yield put(setErrorMessage("Incorrect username or password."));
      break;
  }
}

export function* loginUserEffect() {
  yield takeEvery(UserActionEnum.LOGIN, loginUser);
}
