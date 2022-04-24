import * as actions from "../../reducer";
import * as users from "api/services/users";
import * as api from "api";
import { call, put, take } from "redux-saga/effects";
import { registerUserEffect } from "../registerUser";

describe("registerUserEffect", () => {
  const action = actions.register({
    name: "name",
    password: "password",
    email: "email",
    passwordConfirmation: "passwordConfirmation",
  });
  let saga: any;
  let mock: any;

  beforeEach(() => {
    mock = jest.spyOn(users, "register");
    saga = registerUserEffect();
  });

  afterEach(() => {
    jest.restoreAllMocks();
  });

  [
    {
      responseCode: api.RegisterResponseCode.UserNameAlreadyOccupied,
      message: "User with the same name has already existed",
    },
    {
      responseCode: api.RegisterResponseCode.EmailAlreadyOccupied,
      message: "User with the same email has already existed",
    },
  ].forEach((item, index) => {
    it("should retrun setErrorMessage if response is not correct :: " + index, () => {
      const request: api.RegisterRequest = {
        userName: "name",
        password: "password",
        email: "email",
        passwordConfirmation: "passwordConfirmation",
      };
      const response = {
        responseCode: item.responseCode,
      } as api.RegisterResponse;

      expect(saga.next().value).toStrictEqual(take("user/register"));
      expect(saga.next(action).value).toStrictEqual(call(mock, request));
      expect(saga.next(response).value).toStrictEqual(put(actions.setErrorMessage(item.message)));
      expect(saga.next().done).toBe(true);
    });
  });

  it("should retrun login if response is correct", () => {
    const request: api.RegisterRequest = {
      userName: "name",
      password: "password",
      email: "email",
      passwordConfirmation: "passwordConfirmation",
    };
    const response = {
      responseCode: api.RegisterResponseCode.Successful,
    } as api.RegisterResponse;

    expect(saga.next().value).toStrictEqual(take("user/register"));
    expect(saga.next(action).value).toStrictEqual(call(mock, request));
    expect(saga.next(response).value).toStrictEqual(
      put(actions.login({ userName: "name", password: "password" }))
    );
    expect(saga.next().done).toBe(true);
  });
});
