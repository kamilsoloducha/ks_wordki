import { CardSummary } from "pages/cards/models";
import ReactDOM from "react-dom";
import { act } from "react-dom/test-utils";
import { CardSummaryBuilder } from "test/builders";
import Question from "../Question";

describe("Question", () => {
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
    act(() => {
      ReactDOM.render(<Question value={""} language={0} />, container);
    });

    expect(container.querySelector(".question-main-container")).toBeTruthy();
  });
});
