import { render, unmountComponentAtNode } from "react-dom";
import { act } from "react-dom/test-utils";
import * as redux from "react-redux";
import { BrowserRouter, Redirect, Route, Router } from "react-router-dom";
import LoginPage from "../LoginPage";

fdescribe("LoginPage", () => {
  let container: HTMLDivElement;
  const useSelectorMock = jest.spyOn(redux, "useSelector");
  const useDispatchMock = jest.spyOn(redux, "useDispatch");
  beforeEach(() => {
    useSelectorMock.mockClear();
    useDispatchMock.mockClear();
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
});
