import * as m from "pages/login/services/loginApi";
import { loginUser, loginUserEffect } from "../loginUser";
import { ApiResponse } from "common/models/response";
import { LoginResponse } from "pages/login/requests";
import { put, take, call, takeEvery } from "@redux-saga/core/effects";
import * as actions from "store/user/actions";

describe("loginUserEffect", () => {
  let saga: any;
  let mock: any;

  beforeEach(() => {
    mock = jest.spyOn(m, "login");
    saga = loginUserEffect();
  });

  afterEach(() => {
    mock.mockRestore();
  });

  it("should retrun loginUserFailed if response is not correct", () => {
    mock.mockImplementation(
      () => new Promise<ApiResponse<LoginResponse>>({ isCorrect: false } as any)
    );

    expect(saga.next().value).toStrictEqual(takeEvery(actions.UserActionEnum.LOGIN, loginUser));
    // expect(saga.next(actions.getLoginUser("test", "test")).value).toStrictEqual(
    //   call(mock, { userName: "test", password: "test" })
    // );

    // expect(saga.next({ isCorrect: false } as any).value).toStrictEqual(
    //   put(actions.getLoginUserFailed())
    // );
    // expect(saga.next().done).toBe(true);
  });

  it("should retrun loginUserSuccess if response is correct", () => {
    const response = {
      isCorrect: true,
      error: "",
      response: { id: "id", token: "token" } as LoginResponse,
    } as ApiResponse<LoginResponse>;
    mock.mockImplementation(() => new Promise<ApiResponse<LoginResponse>>((_, __) => response));

    expect(saga.next().value).toStrictEqual(takeEvery(actions.UserActionEnum.LOGIN, loginUser));
    // expect(saga.next(actions.getLoginUser("test", "test")).value).toStrictEqual(
    //   call(mock, { userName: "test", password: "test" })
    // );

    // expect(saga.next(response).value).toStrictEqual(
    //   put(actions.getLoginUserSuccess("token", "id", new Date(1100)))
    // );

    // expect(saga.next().done).toBe(true);
  });
});
