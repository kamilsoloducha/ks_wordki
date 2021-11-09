import { call, put, takeLatest } from "@redux-saga/core/effects";
import { ApiResponse } from "common/models/response";
import { UserData } from "common/models/userModel";
import http from "common/services/http/http";
import LoginResponse from "pages/login/models/loginResponse";
import { requestFailed } from "store/root/actions";
import { getLoginUserSuccess, LoginUser, UserActionEnum } from "../actions";

function fetchData(userName: string, password: string) {
  const request = { userName, password };
  return http
    .put<ApiResponse<LoginResponse>>("/users/login", request)
    .then((response) => ({ data: response.data }))
    .catch((error: Error) => ({ error }));
}

function* loginUser(action: LoginUser) {
  const { data, error }: { data: ApiResponse<LoginResponse>; error: any } =
    yield call(() => fetchData(action.name, action.password));
  if (data.isCorrect) {
    const userData: UserData = {
      id: data.response.id,
      token: data.response.token,
    };
    localStorage.setItem("user", JSON.stringify(userData));
  }
  yield put(
    data
      ? getLoginUserSuccess(
          data.response.token,
          data.response.id,
          new Date(1100)
        )
      : requestFailed(error)
  );
}

export default function* loginUserEffect() {
  yield takeLatest(UserActionEnum.LOGIN, loginUser);
}
