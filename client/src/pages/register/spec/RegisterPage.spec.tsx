import * as redux from "react-redux";
import { ReactElement } from "react";
import configureMockStore, { MockStoreEnhanced } from "redux-mock-store";
import RegisterPage from "../RegisterPage";
import UserState from "store/user/state";
import { render } from "@testing-library/react";

const mockedUsedNavigate = jest.fn();

jest.mock('react-router-dom', () => ({
  ...jest.requireActual('react-router-dom') as any,
  useNavigate: () => mockedUsedNavigate,
}));

describe("GroupsPage", () => {
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
        <RegisterPage />
      </redux.Provider>
    )
  });

  afterEach(() => {
    mockStore.clearActions();
    jest.clearAllMocks();
  });

  it("should be created", () => {

    const { container } = render(component);

    expect(container.querySelector(".register-page-container")).toBeTruthy();
  });
});
