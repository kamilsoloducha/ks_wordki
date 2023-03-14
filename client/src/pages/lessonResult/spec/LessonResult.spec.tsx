import * as redux from "react-redux";
import { act } from "react-dom/test-utils";
import { ReactElement } from "react";
import configureMockStore, { MockStoreEnhanced } from "redux-mock-store";
import { LessonSettings } from "pages/lessonSettings/models/lessonSettings";
import { SetLesson } from "pages/lesson/models/lessonState";
import Results from "pages/lesson/models/results";
import LessonResult from "../LessonResult";
import LessonState from "store/lesson/state";
import { render } from "@testing-library/react";

const mockedUsedNavigate = jest.fn();

jest.mock("react-router-dom", () => ({
  ...(jest.requireActual("react-router-dom") as any),
  useNavigate: () => mockedUsedNavigate,
}));

describe("GroupsPage", () => {
  let mockState: LessonState;
  let mockStore: MockStoreEnhanced<any, any>;
  let component: ReactElement;
  let container: HTMLElement;

  beforeEach(() => {
    mockState = {
      isProcessing: false,
      repeats: [{} as any],
      lessonState: SetLesson,
      isCorrect: null,
      isSecondChangeUsed: false,
      answer: "",
      cardsCount: null,
      results: {} as Results,
      lessonCount: 0,
      lessonType: 0,
      settings: {
        mode: 1,
        count: 0,
        languages: [],
        type: -1,
        groups: [],
        selectedGroupId: null,
        wrongLimit: 15,
      } as LessonSettings,
      lessonHistory: [],
      languages: [],
    };

    mockStore = configureMockStore([])({ lessonReducer: mockState });

    component = (
      <redux.Provider store={mockStore}>
        <LessonResult />
      </redux.Provider>
    );
  });

  afterEach(() => {
    mockStore.clearActions();
    jest.clearAllMocks();
  });

  it("should be rendered", () => {
    act(() => {
      container = render(component).container;
    });
    expect(container).toBeTruthy();
    expect(container.querySelector(".lesson-results-container")).toBeTruthy();
  });
});
