import { render } from "@testing-library/react";
import { LessonStatus, StartLessonPending } from "pages/lesson/models/lessonState";
import { act } from "react-dom/test-utils";
import { Provider } from "react-redux";
import createMockStore from "redux-mock-store";
import LessonController from "../LessonController";

describe("LessonController", () => {
  const mockStore = createMockStore([])({});
  let container: HTMLElement;

  beforeEach(() => {
  });

  afterEach(() => {
    mockStore.clearActions;
    jest.clearAllMocks();
  });

  it("should be created", async () => {
    const lessonState: LessonStatus = StartLessonPending;
    act(() => {
      container = render(
        <Provider store={mockStore}>
          <LessonController lessonState={lessonState} />
        </Provider>
      ).container;
    });

    expect(container.querySelector("button")).toBeTruthy();
  });
});
