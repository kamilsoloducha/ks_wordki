import { call, put, take } from "@redux-saga/core/effects";
import { PayloadAction } from "@reduxjs/toolkit";
import * as api from "api";
import { SagaIterator } from "redux-saga";
import { LoginPayload } from "../action-payload";
import { loginSuccess, setErrorMessage } from "../reducer";

export function* loginUserEffect(): SagaIterator {
  while (true) {
    const action: PayloadAction<LoginPayload> = yield take("user/login");
    const request = {
      userName: action.payload.userName,
      password: action.payload.password,
    } as api.LoginRequest;
    const apiResponse: api.LoginResponse = yield call(api.login, request);

    switch (apiResponse.responseCode) {
      case api.LoginResponseCode.Successful:
        localStorage.setItem("id", apiResponse.id);
        localStorage.setItem("token", apiResponse.token);
        localStorage.setItem("creationDate", apiResponse.creatingDateTime);
        localStorage.setItem("expirationDate", apiResponse.expirationDateTime);
        yield put(
          loginSuccess({
            token: apiResponse.token,
            id: apiResponse.id,
            expirationDate: apiResponse.expirationDateTime,
          })
        );
        break;
      case api.LoginResponseCode.UserNotFound:
        yield put(setErrorMessage("Incorrect username or password."));
        break;
    }
  }
}