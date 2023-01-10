import { render } from "@testing-library/react";
import { CardSummary } from "pages/cards/models";
import ReactDOM from "react-dom";
import { act } from "react-dom/test-utils";
import { CardSummaryBuilder } from "test/builders";
import Question from "../Question";

describe("Question", () => {
  const mockFunc = jest.fn((item: CardSummary) => {});

  beforeEach(() => {
  });

  afterEach(() => {
    mockFunc.mockClear();
  });

  it("should be created", async () => {
    let container = {} as HTMLElement;
    act(() => {
      container = render(<Question value={""} language={0} />).container;
    });

    expect(container.querySelector(".question-main-container")).toBeTruthy();
  });
});
