import { render, unmountComponentAtNode } from "react-dom";
import { fireEvent } from "@testing-library/react";
import { act } from "react-dom/test-utils";
import * as redux from "react-redux";
import { BrowserRouter, Route } from "react-router-dom";
import LoginPage from "../LoginPage";
import { UserActionEnum } from "store/user/actions";

describe("LoginPage", () => {
  let container: HTMLDivElement;
  const useSelectorMock = jest.spyOn(redux, "useSelector");
  const useDispatchMock = jest.spyOn(redux, "useDispatch");
  const mockFunc = jest.fn(() => {});

  beforeEach(() => {
    useSelectorMock.mockClear();
    useDispatchMock.mockClear();
    mockFunc.mockClear();
    useDispatchMock.mockReturnValue(mockFunc as any);

    container = document.createElement("div");
    document.body.appendChild(container);
  });

  afterEach(() => {
    unmountComponentAtNode(container);
    container.remove();
  });

  it("LoginPage", async () => {
    useSelectorMock.mockReturnValueOnce(null);
    useSelectorMock.mockReturnValueOnce(false);
    useSelectorMock.mockReturnValueOnce(null);
    act(() => {
      render(<LoginPage />, container);
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

  it("LoginPage", async () => {
    useSelectorMock.mockReturnValueOnce(null);
    useSelectorMock.mockReturnValueOnce(true);
    act(() => {
      render(<LoginPage />, container);
    });
    const inputs = container.querySelectorAll("input");
    inputs.forEach((item) => expect(item.disabled).toBe(true));
  });

  it("LoginPage", async () => {
    useSelectorMock.mockReturnValueOnce("userId");
    useSelectorMock.mockReturnValueOnce(false);
    act(() => {
      render(
        <BrowserRouter>
          <LoginPage />
          <Route path="/dashboard">Dashboard</Route>
        </BrowserRouter>,
        container
      );
    });
    expect(container.innerHTML).toContain("Dashboard");
  });

  it("LoginPage", async () => {
    useSelectorMock.mockReturnValueOnce(null);
    useSelectorMock.mockReturnValueOnce(false);
    act(() => {
      render(<LoginPage />, container);
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
