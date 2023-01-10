import { render } from "@testing-library/react";
import { CardSummary } from "pages/cards/models";
import ReactDOM from "react-dom";
import { act } from "react-dom/test-utils";
import { History } from "../History";

describe("History", () => {
  const mockFunc = jest.fn((item: CardSummary) => {});

  beforeEach(() => {
  });

  afterEach(() => {
    mockFunc.mockClear();
  });

  it("should responce on onClick", async () => {
    let container = {} as HTMLElement;
    act(() => {
      container = render(<History history={[]} />).container;
    });

    expect(container.querySelector("table")).toBeTruthy();
  });
});
