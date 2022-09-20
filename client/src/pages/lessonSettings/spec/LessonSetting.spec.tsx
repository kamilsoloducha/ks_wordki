import * as redux from "react-redux";
import { ReactElement } from "react";
import configureMockStore from 'redux-mock-store';
import { LessonStatus } from "pages/lesson/models/lessonState";
import Results from "pages/lesson/models/results";
import LessonSettingsPage from "../LessonSetting";
import { LessonSettings } from "../models/lessonSettings";
import { render } from "@testing-library/react";
import LessonState from "store/lesson/state";

const mockedUsedNavigate = jest.fn();

jest.mock('react-router-dom', () => ({
  ...jest.requireActual('react-router-dom') as any,
  useNavigate: () => mockedUsedNavigate,
}));

describe("GroupsPage", () => {
  let component: ReactElement;

  const mockState: LessonState = {
    isProcessing: false,
    repeats: [],
    lessonState: {} as LessonStatus,
    isCorrect: null,
    isSecondChangeUsed: false,
    answer: "",
    cardsCount: null,
    lessonCount: 0,
    lessonType: 0,
    results: {} as Results,
    settings: {} as LessonSettings,
    lessonHistory: []
  };

  const mockStore = configureMockStore([])({ lessonReducer: mockState });;

  beforeEach(() => {
    component = (
      <redux.Provider store={mockStore}>
        <LessonSettingsPage />
      </redux.Provider>
    );
  });

  afterEach(() => {
    jest.clearAllMocks();
    mockStore.clearActions();
  });

  it("should hide spinner", () => {
    mockState.isProcessing = false;

    const { container } = render(component);

    expect(container.querySelector(".loader")).toBeFalsy();
  });

  it("should show spinner", () => {
    mockState.isProcessing = true;

    const { container } = render(component);

    expect(container.querySelector(".loader")).toBeTruthy();
  });
});
