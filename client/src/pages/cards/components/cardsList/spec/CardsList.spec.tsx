import { CardSummary } from "pages/cards/models";
import ReactDOM from "react-dom";
import { act } from "react-dom/test-utils";
import { CardSummaryBuilder } from "test/builders";
import CardsList from "../CardsList";

describe("CardsList", () => {
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
    const cards = [new CardSummaryBuilder().build()];
    act(() => {
      ReactDOM.render(
        <CardsList cards={cards} onItemSelected={(item) => mockFunc(item)} />,
        container
      );
    });

    const submitButton = container.querySelector(".card-item") as HTMLElement;
    await act(async () => {
      submitButton.click();
    });
    expect(mockFunc).toHaveBeenCalledTimes(1);
    expect(mockFunc).toHaveBeenCalledWith(cards[0]);
  });
});
