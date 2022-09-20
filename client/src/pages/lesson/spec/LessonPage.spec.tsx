import * as redux from "react-redux";
import { act } from "react-dom/test-utils";
import { ReactElement } from "react";
import configureMockStore, { MockStoreEnhanced } from "redux-mock-store";
import LessonPage from "../LessonPage";
import LessonState, { initialState } from "store/lesson/state";
import { render } from "@testing-library/react";


const mockedUsedNavigate = jest.fn();

jest.mock('react-router-dom', () => ({
  ...jest.requireActual('react-router-dom') as any,
  useNavigate: () => mockedUsedNavigate,
}));

describe("GroupsPage", () => {
  let mockState: LessonState;
  let mockStore: MockStoreEnhanced<any, any>;
  let component: ReactElement;
  let container: HTMLElement;

  beforeEach(() => {
    mockState = { ...initialState };

    mockStore = configureMockStore([])({ lessonReducer: mockState });

    component = (
      <redux.Provider store={mockStore}>
        <LessonPage />
      </redux.Provider>
    );
  });

  afterEach(() => {
    mockStore.clearActions();
    jest.clearAllMocks();
  });

  it("should display spinner if it is loading", () => {
    act(() => {
      container = render(component).container;
    });

    expect(container.querySelector(".lesson-page")).toBeTruthy();
  });
});
