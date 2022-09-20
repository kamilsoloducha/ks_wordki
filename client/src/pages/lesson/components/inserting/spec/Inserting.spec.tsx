import * as redux from "react-redux";
import { act } from "react-dom/test-utils";
import { ReactElement } from "react";
import configureMockStore, { MockStoreEnhanced } from "redux-mock-store";
import LessonState, { initialState } from "store/lesson/state";
import { render } from "@testing-library/react";
import Inserting from "../Inserting";
import { CheckPending } from "pages/lesson/models/lessonState";

jest.mock("../../answer/Answer", () => () => <div>Answer</div>);

describe("Inserting", () => {
  let mockState: LessonState;
  let mockStore: MockStoreEnhanced<any, any>;
  let component: ReactElement;
  let container: HTMLElement;

  beforeEach(() => {
    mockState = {
      ...initialState,
      repeats: [{} as any],
      lessonState: CheckPending,
      answer:"test"
    };

    mockStore = configureMockStore([])({ lessonReducer: mockState });

    component = (
      <redux.Provider store={mockStore}>
        <Inserting />
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

    expect(container.querySelector("input")).toBeTruthy();
  });
});
