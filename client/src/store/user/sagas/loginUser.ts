import { call, put, takeEvery } from "@redux-saga/core/effects";
import { ApiResponse } from "common/models/response";
import { UserData } from "common/models/userModel";
import { LoginRequest, LoginResponse } from "pages/login/requests";
import { login } from "pages/login/services/loginApi";
import { getLoginUserFailed, getLoginUserSuccess, LoginUser, UserActionEnum } from "../actions";

export function* loginUser(action: LoginUser) {
  const request = {
    userName: action.name,
    password: action.password,
  } as LoginRequest;
  const apiResponse: ApiResponse<LoginResponse> = yield call(login, request);

  if (apiResponse.isCorrect) {
    const userData: UserData = {
      id: apiResponse.response.id,
      token: apiResponse.response.token,
    };
    localStorage.setItem("user", JSON.stringify(userData));
  }

  yield put(
    apiResponse.isCorrect
      ? getLoginUserSuccess(apiResponse.response.token, apiResponse.response.id, new Date(1100))
      : getLoginUserFailed()
  );
}

export function* loginUserEffect() {
  yield takeEvery(UserActionEnum.LOGIN, loginUser);
}
