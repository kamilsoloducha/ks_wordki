import { render, unmountComponentAtNode } from "react-dom";
import { fireEvent } from "@testing-library/react";
import { act } from "react-dom/test-utils";
import * as redux from "react-redux";
import { BrowserRouter, Route } from "react-router-dom";
import LoginPage from "../LoginPage";
import { UserActionEnum } from "store/user/actions";
import configureStore from "redux-mock-store";

describe("LoginPage", () => {
  let container: HTMLDivElement;
  const useDispatchMock = jest.spyOn(redux, "useDispatch");
  const mockFunc = jest.fn(() => {});
  const mockStore = configureStore([]);
  let store: any;

  beforeEach(() => {
    useDispatchMock.mockClear();
    mockFunc.mockClear();
    useDispatchMock.mockReturnValue(mockFunc as any);

    container = document.createElement("div");
    document.body.appendChild(container);

    store = mockStore({
      userReducer: {
        isLogin: false,
        isLoading: false,
        id: "",
      },
    });
  });

  afterEach(() => {
    unmountComponentAtNode(container);
    container.remove();
  });

  it("LoginPage1", async () => {
    act(() => {
      render(
        <redux.Provider store={store}>
          <LoginPage />
        </redux.Provider>,
        container
      );
    });
    expect(container.querySelectorAll("input").length).toBe(3);
    expect(container.querySelectorAll("input[type=submit]").length).toBe(1);
    expect(container.querySelectorAll(".error-message").length).toBe(0);

    const submitButton = container.querySelector("input[type=submit]") as HTMLElement;
    await act(async () => {
      submitButton.click();
    });

    expect(container.querySelectorAll(".error-message").length).toBe(2);
  });

  it("LoginPage2", async () => {
    store = mockStore({
      userReducer: {
        isLogin: false,
        isLoading: true,
        token: "",
        id: "",
        expirationDate: new Date(1),
        errorMessage: "",
      },
    });
    act(() => {
      render(
        <redux.Provider store={store}>
          <LoginPage />
        </redux.Provider>,
        container
      );
    });
    const inputs = container.querySelectorAll("input");
    inputs.forEach((item) => expect(item.disabled).toBe(true));
  });

  it("LoginPage3", async () => {
    store = mockStore({
      userReducer: {
        isLogin: true,
        isLoading: false,
        token: "",
        id: "test",
        expirationDate: new Date(1),
        errorMessage: "",
      },
    });
    act(() => {
      render(
        <redux.Provider store={store}>
          <BrowserRouter>
            <LoginPage />
            <Route path="/dashboard">Dashboard</Route>
          </BrowserRouter>
        </redux.Provider>,
        container
      );
    });
    expect(container.innerHTML).toContain("Dashboard");
  });

  it("LoginPage4", async () => {
    act(() => {
      render(
        <redux.Provider store={store}>
          <LoginPage />
        </redux.Provider>,
        container
      );
    });

    fireEvent.change(container.querySelector("#userName") as HTMLInputElement, {
      target: {
        value: "testUser",
      },
    });

    fireEvent.change(container.querySelector("#password") as HTMLInputElement, {
      target: {
        value: "testPassword",
      },
    });

    const submitButton = container.querySelector("input[type=submit]") as HTMLElement;
    await act(async () => {
      submitButton.click();
    });

    expect(container.querySelectorAll(".error-message").length).toBe(0);
    expect(mockFunc).toHaveBeenCalledTimes(2);
    expect(mockFunc).toHaveBeenCalledWith({
      name: "testUser",
      password: "testPassword",
      type: UserActionEnum.LOGIN,
    });
  });
});
