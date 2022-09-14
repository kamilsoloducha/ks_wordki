import * as redux from "react-redux";
import ReactDOM from "react-dom";
import { act } from "react-dom/test-utils";
import { ReactElement } from "react";
import configureStore from "redux-mock-store";
import { MainState } from "store/store";
import { LessonSettings } from "pages/lessonSettings/models/lessonSettings";
import { SetLesson } from "pages/lesson/models/lessonState";
import Results from "pages/lesson/models/results";
import NewCardsSettings from "../NewCardsSettings";

describe("GroupsPage", () => {
  let container: HTMLDivElement;
  let component: ReactElement;
  const dispatchMock = jest.fn(() => {});
  let mockStore: MainState;
  let store: any;

  beforeEach(() => {
    container = document.createElement("div");
    document.body.appendChild(container);

    mockStore = {
      lessonReducer: {
        isProcessing: false,
        repeats: [{}],
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
      },
    } as any;

    store = configureStore([])(mockStore);

    component = (
      <>
        <redux.Provider store={store}>
          <NewCardsSettings />
        </redux.Provider>
      </>
    );
  });

  afterEach(() => {
    document.body.removeChild(container);
    container.remove();
  });

  it("should be created", () => {
    act(() => {
      ReactDOM.render(component, container);
    });

    expect(container.querySelector(".setting-item")).toBeTruthy();
  });
});
