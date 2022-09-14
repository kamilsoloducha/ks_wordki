import * as redux from "react-redux";
import ReactDOM from "react-dom";
import { act } from "react-dom/test-utils";
import { ReactElement } from "react";
import configureStore from "redux-mock-store";
import { MainState } from "store/store";
import LessonPage from "../LessonPage";
import { SetLesson } from "../models/lessonState";
import Results from "../models/results";
import { LessonSettings } from "pages/lessonSettings/models/lessonSettings";
import { BrowserRouter } from "react-router-dom";


fdescribe("GroupsPage", () => {
  let container: HTMLDivElement;
  let component: ReactElement;
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
          <BrowserRouter>
            <LessonPage />
          </BrowserRouter>
        </redux.Provider>
      </>
    );
  });

  afterEach(() => {
    document.body.removeChild(container);
    container.remove();
  });

  // it("should display spinner if it is loading", () => {
  //   act(() => {
  //     ReactDOM.render(component, container);
  //   });

  //   expect(container.querySelector(".lesson-page")).toBeTruthy();
  // });
});
