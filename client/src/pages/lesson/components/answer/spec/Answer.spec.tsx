import { CardSummary } from "pages/cards/models";
import ReactDOM from "react-dom";
import { act } from "react-dom/test-utils";
import { CardSummaryBuilder } from "test/builders";
import Answer from "../Answer";

describe("Answer", () => {
  let container: HTMLDivElement;
  const mockFunc = jest.fn((item: CardSummary) => {});

  beforeEach(() => {
    container = document.createElement("div");
    document.body.appendChild(container);
  });

  afterEach(() => {
    document.body.removeChild(container);
    container.remove();
    mockFunc.mockClear();
  });

  it("should be created", async () => {
    const isVisible = true;
    const userAnwer = "test";
    const correctAnswer = "test";
    const exampleAnswer = "test";
    act(() => {
      ReactDOM.render(
        <Answer
          isVisible={isVisible}
          userAnswer={userAnwer}
          correctAnswer={correctAnswer}
          exampleAnswer={exampleAnswer}
        />,
        container
      );
    });

    expect(container.querySelector(".correct-answer")).toBeTruthy();
  });
});
