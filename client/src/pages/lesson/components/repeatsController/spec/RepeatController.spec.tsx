import { render } from "@testing-library/react";
import { AnswerPending } from "pages/lesson/models/lessonState";
import ReactDOM from "react-dom";
import { act } from "react-dom/test-utils";
import RepeatsController from "../RepeatsController";

describe("RepeatController", () => {
  const onCheckMock = jest.fn(() => {});
  const onCorrectMock = jest.fn(() => {});
  const onWrongMock = jest.fn(() => {});

  beforeEach(() => {
  });

  afterEach(() => {
    onCheckMock.mockClear();
    onCorrectMock.mockClear();
    onWrongMock.mockClear();
  });

  it("should be created", async () => {
    let container = {} as HTMLElement;
    act(() => {
      container = render(
        <RepeatsController
          isCorrect={true}
          lessonState={AnswerPending}
          onCheckClick={onCheckMock}
          onCorrectClick={onCorrectMock}
          onWrongClick={onWrongMock}
        />
      ).container;
    });

    expect(container.querySelector(".repeats-controller-container")).toBeTruthy();
  });
});
