import * as redux from "react-redux";
import { CardSummary } from "pages/cards/models";
import { LessonStatus, StartLessonPending } from "pages/lesson/models/lessonState";
import ReactDOM from "react-dom";
import { act } from "react-dom/test-utils";
import LessonController from "../LessonController";

describe("LessonController", () => {
  let container: HTMLDivElement;
  const useDispatchMock = jest.spyOn(redux, "useDispatch");
  const dispatchMock = jest.fn(() => {});

  beforeEach(() => {
    dispatchMock.mockClear();

    useDispatchMock.mockClear();
    useDispatchMock.mockReturnValue(dispatchMock as any);

    container = document.createElement("div");
    document.body.appendChild(container);
  });

  afterEach(() => {
    document.body.removeChild(container);
    container.remove();
  });

  it("should be created", async () => {
    const lessonState: LessonStatus = StartLessonPending;
    act(() => {
      ReactDOM.render(<LessonController lessonState={lessonState} />, container);
    });

    expect(container.querySelector("button")).toBeTruthy();
  });
});
