import { CardSummary } from "pages/cards/models";
import ReactDOM from "react-dom";
import { act } from "react-dom/test-utils";
import { CardSummaryBuilder } from "test/builders";
import { CardItem } from "../CardItem";

describe("CardItem", () => {
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
    const card = new CardSummaryBuilder().build();
    act(() => {
      ReactDOM.render(<CardItem card={card} onClick={(item) => mockFunc(item)} />, container);
    });

    const submitButton = container.querySelector(".row-container") as HTMLElement;
    await act(async () => {
      submitButton.click();
    });
    expect(mockFunc).toHaveBeenCalledTimes(1);
    expect(mockFunc).toHaveBeenCalledWith(card);
  });
});
