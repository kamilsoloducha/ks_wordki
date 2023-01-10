import { render } from "@testing-library/react";
import { CardSummary } from "pages/cards/models";
import ReactDOM from "react-dom";
import { act } from "react-dom/test-utils";
import { CardSummaryBuilder } from "test/builders";
import Answer from "../Answer";

describe("Answer", () => {
  const mockFunc = jest.fn((item: CardSummary) => {});

  beforeEach(() => {
  });

  afterEach(() => {
    mockFunc.mockClear();
  });

  it("should be created", async () => {
    let container = {} as HTMLElement;
    const isVisible = true;
    const userAnwer = "test";
    const correctAnswer = "test";
    const exampleAnswer = "test";
    act(() => {
      container = render(
        <Answer
          isVisible={isVisible}
          userAnswer={userAnwer}
          correctAnswer={correctAnswer}
          exampleAnswer={exampleAnswer}
        />
      ).container;
    });

    expect(container.querySelector(".correct-answer")).toBeTruthy();
  });
});
