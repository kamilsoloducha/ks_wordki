import { fireEvent, render } from "@testing-library/react";
import { act } from "react-dom/test-utils";
import * as redux from "react-redux";
import LoginPage from "../LoginPage";
import UserState from "store/user/state";
import configureMockStore, { MockStoreEnhanced } from 'redux-mock-store';
import { ReactElement } from "react";
import { login } from "store/user/reducer";

const mockedUsedNavigate = jest.fn();

jest.mock('react-router-dom', () => ({
  ...jest.requireActual('react-router-dom') as any,
  useNavigate: () => mockedUsedNavigate,
}));


describe("LoginPage", () => {
  let mockState: UserState;
  let mockStore: MockStoreEnhanced<unknown, {}>;
  let component: ReactElement;

  beforeEach(() => {
    mockState = {
      isLogin: false,
      isLoading: false,
      token: "",
      id: "",
      expirationDate: "",
      errorMessage: ""
    }
    mockStore = configureMockStore([])({userReducer: mockState});

    component = (
      <redux.Provider store={mockStore}>
        <LoginPage />
      </redux.Provider>
    )
  });

  afterEach(() => {
    mockStore.clearActions();
    jest.clearAllMocks();
  });

  it("should be rendered", async () => {
    const { container } = render(component);

    expect(container.querySelectorAll("input").length).toBe(3);
    expect(container.querySelectorAll("input[type=submit]").length).toBe(1);
    expect(container.querySelectorAll(".error-message").length).toBe(0);

    const submitButton = container.querySelector("input[type=submit]") as HTMLElement;
    await act(async () => {
      submitButton.click();
    });

    expect(container.querySelectorAll(".error-message").length).toBe(2);
  });

  it("should disable inputs when is loading", async () => {
    mockState.isLoading = true;

    const { container } = render(component);

    const inputs = container.querySelectorAll("input");
    inputs.forEach((item) => expect(item.disabled).toBe(true));
  });

  it("should navigate to dashboard when is logged in", async () => {
    mockState.isLogin = true;
    mockState.id = "test";
    mockState.expirationDate = new Date(1).toDateString();
    render(component);

    expect(mockedUsedNavigate.mock.calls[0][0]).toBe("/dashboard");
  });

  it("should dispatch login action", async () => {
    const { container } = render(component);

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

    expect(mockStore.getActions()[1]).toStrictEqual(login({password:"testPassword", userName:"testUser"}));
  });
});
