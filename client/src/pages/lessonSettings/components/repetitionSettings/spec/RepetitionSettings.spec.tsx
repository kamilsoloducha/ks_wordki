import * as redux from "react-redux";
import { act } from "react-dom/test-utils";
import { ReactElement } from "react";
import configureMockStore, { MockStoreEnhanced } from "redux-mock-store";
import LessonState, { initialState } from "store/lesson/state";
import { render } from "@testing-library/react";
import Settings from "../RepetitionSettings";

describe("Fiszka", () => {
  let mockState: LessonState;
  let mockStore: MockStoreEnhanced<any, any>;
  let component: ReactElement;
  let container: HTMLElement;

  beforeEach(() => {
    mockState = { ...initialState };

    mockStore = configureMockStore([])({ lessonReducer: mockState });

    component = (
      <redux.Provider store={mockStore}>
        <Settings />
      </redux.Provider>
    );
  });

  afterEach(() => {
    mockStore.clearActions();
    jest.clearAllMocks();
  });

  it("should be created", () => {
    act(() => {
      container = render(component).container;
    });

    expect(container.querySelectorAll(".setting-item").length).toBe(3);
  });
});
