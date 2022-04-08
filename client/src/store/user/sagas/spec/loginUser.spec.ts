import * as users from "api/services/users";
import * as api from "api";
import { put, call, take } from "@redux-saga/core/effects";
import * as actions from "store/user/actions";
import { loginUserEffect } from "../loginUser";

describe("loginUserEffect", () => {
  const action = { name: "testName", password: "testPassword", type: actions.UserActionEnum.LOGIN };
  let saga: any;
  let mock: any;

  beforeEach(() => {
    mock = jest.spyOn(users, "login");
    saga = loginUserEffect();
  });

  afterEach(() => {
    jest.restoreAllMocks();
  });

  it("should retrun loginUserFailed if response is not correct", () => {
    const request: api.LoginRequest = { userName: "testName", password: "testPassword" };
    const response = { responseCode: api.LoginResponseCode.UserNotFound } as api.LoginResponse;
    mock.mockImplementation(() => new Promise<api.LoginResponse>(() => response));

    expect(saga.next().value).toStrictEqual(take(actions.UserActionEnum.LOGIN));
    expect(saga.next(action).value).toStrictEqual(call(mock, request));
    expect(saga.next(response).value).toStrictEqual(
      put(actions.setErrorMessage("Incorrect username or password."))
    );
    expect(saga.next().done).toBe(true);
  });

  it("should retrun loginUserSuccess if response is correct", () => {
    const setItemMock = jest.fn();
    jest.spyOn(window.localStorage.__proto__, "setItem");
    window.localStorage.__proto__.setItem = setItemMock;

    const request: api.LoginRequest = { userName: "testName", password: "testPassword" };
    const response = {
      responseCode: api.LoginResponseCode.Successful,
      token: "token",
      id: "id",
      expirationDateTime: "2",
    } as api.LoginResponse;

    expect(saga.next().value).toStrictEqual(take(actions.UserActionEnum.LOGIN));
    expect(saga.next(action).value).toStrictEqual(call(mock, request));

    expect(saga.next(response).value).toStrictEqual(
      put(actions.loginSuccess("token", "id", new Date("2")))
    );

    expect(saga.next().done).toBe(true);

    expect(setItemMock).toHaveBeenCalledWith("id", response.id);
    expect(setItemMock).toHaveBeenCalledWith("token", response.token);
    expect(setItemMock).toHaveBeenCalledWith("creationDate", response.creatingDateTime);
    expect(setItemMock).toHaveBeenCalledWith("expirationDate", response.expirationDateTime);
  });

  // it("test", () => {
  //   const action = {
  //     name: "testName",
  //     password: "testPassword",
  //     type: actions.UserActionEnum.LOGIN,
  //   };
  //   const response = { responseCode: api.LoginResponseCode.UserNotFound } as api.LoginResponse;
  //   mock.mockImplementation(() => new Promise<api.LoginResponse>(() => response));

  //   testSaga(loginUser, action)
  //     .next()
  //     .call(mock, { userName: action.name, password: action.password })
  //     .next(response)
  //     .put(actions.setErrorMessage("Incorrect username or password."))
  //     .next()
  //     .isDone();
  // });
});
