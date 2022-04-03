import { testSaga } from "redux-saga-test-plan";
import * as m from "pages/login/services/loginApi";
import { loginUser, loginUserEffect } from "../loginUser";
import { ApiResponse } from "common/models/response";
import { LoginResponse } from "pages/login/models";
import { put, call, takeEvery } from "@redux-saga/core/effects";
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

  it("should call take every", () => {
    mock.mockImplementation(
      () => new Promise<ApiResponse<LoginResponse>>({ isCorrect: false } as any)
    );

    expect(saga.next().value).toStrictEqual(takeEvery(actions.UserActionEnum.LOGIN, loginUser));
  });
});

describe("loginUser generator", () => {
  let saga: any;
  let mock: any;

  beforeEach(() => {
    mock = jest.spyOn(m, "login");
    saga = loginUser({
      name: "test",
      password: "testwf",
      type: actions.UserActionEnum.LOGIN,
    });
  });

  afterEach(() => {
    mock.mockRestore();
  });

  it("should retrun loginUserFailed if response is not correct", () => {
    const response = {
      isCorrect: false,
      error: "",
      response: { id: "id", token: "token" } as LoginResponse,
    } as ApiResponse<LoginResponse>;
    mock.mockImplementation(() => new Promise<ApiResponse<LoginResponse>>((_, __) => response));

    expect(saga.next().value.toString()).toBe(call(() => mock).toString());
    // expect(mock).toHaveBeenCalledTimes(1);

    expect(saga.next({ isCorrect: false } as any).value).toStrictEqual(
      put(actions.setErrorMessage())
    );
    expect(saga.next().done).toBe(true);
  });

  it("should retrun loginUserSuccess if response is correct", () => {
    const response = {
      isCorrect: true,
      error: "",
      response: { id: "id", token: "token" } as LoginResponse,
    } as ApiResponse<LoginResponse>;
    mock.mockImplementation(() => new Promise<ApiResponse<LoginResponse>>((_, __) => response));

    expect(saga.next().value.toString()).toBe(call(() => mock).toString());

    expect(saga.next(response).value).toStrictEqual(
      put(actions.getLoginUserSuccess("token", "id", new Date(1100)))
    );

    expect(saga.next().done).toBe(true);
  });
});

it("test", () => {
  const action = {
    name: "testName",
    password: "testPassword",
    type: actions.UserActionEnum.LOGIN,
  };
  let mock = jest.spyOn(m, "login");
  mock.mockImplementation(
    () => new Promise<ApiResponse<LoginResponse>>({ isCorrect: false } as any)
  );

  testSaga(loginUser, action)
    .next()
    .call(mock as any, { userName: action.name, password: action.password })
    .next(action)
    .put({ type: actions.UserActionEnum.LOGIN_SUCCESS })
    .next()
    .isDone();
});
