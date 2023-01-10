import { render } from "@testing-library/react";
import { CardSummary } from "pages/cards/models";
import ReactDOM from "react-dom";
import { act } from "react-dom/test-utils";
import { CardSummaryBuilder } from "test/builders";
import CardsList from "../CardsList";

describe("CardsList", () => {
  let container: HTMLElement;
  const mockFunc = jest.fn((item: CardSummary) => {});

  beforeEach(() => {
  });

  afterEach(() => {
    mockFunc.mockClear();
  });

  it("should responce on onClick", async () => {
    const cards = [new CardSummaryBuilder().build()];
    act(() => {
      container = render(<CardsList cards={cards} onItemSelected={(item) => mockFunc(item)} />).container;
    });

    const submitButton = container.querySelector(".card-item") as HTMLElement;
    await act(async () => {
      submitButton.click();
    });
    expect(mockFunc).toHaveBeenCalledTimes(1);
    expect(mockFunc).toHaveBeenCalledWith(cards[0]);
  });
});
