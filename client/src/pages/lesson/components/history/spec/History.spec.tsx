import { CardSummary } from "pages/cards/models";
import ReactDOM from "react-dom";
import { act } from "react-dom/test-utils";
import { History } from "../History";

describe("History", () => {
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

  it("should responce on onClick", async () => {
    act(() => {
      ReactDOM.render(<History history={[]} />, container);
    });

    expect(container.querySelector("table")).toBeTruthy();
  });
});
