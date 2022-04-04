import { CardSummary } from "pages/cards/models";
import { AnswerPending } from "pages/lesson/models/lessonState";
import ReactDOM from "react-dom";
import { act } from "react-dom/test-utils";
import { CardSummaryBuilder } from "test/builders";
import RepeatsController from "../RepeatsController";

describe("RepeatController", () => {
  let container: HTMLDivElement;
  const onCheckMock = jest.fn(() => {});
  const onCorrectMock = jest.fn(() => {});
  const onWrongMock = jest.fn(() => {});

  beforeEach(() => {
    container = document.createElement("div");
    document.body.appendChild(container);
  });

  afterEach(() => {
    document.body.removeChild(container);
    container.remove();
    onCheckMock.mockClear();
    onCorrectMock.mockClear();
    onWrongMock.mockClear();
  });

  it("should be created", async () => {
    act(() => {
      ReactDOM.render(
        <RepeatsController
          isCorrect={true}
          lessonState={AnswerPending}
          onCheckClick={onCheckMock}
          onCorrectClick={onCorrectMock}
          onWrongClick={onWrongMock}
        />,
        container
      );
    });

    expect(container.querySelector(".repeats-controller-container")).toBeTruthy();
  });
});
