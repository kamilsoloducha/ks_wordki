import { render } from "@testing-library/react";
import { CardSummary } from "pages/cards/models";
import { act } from "react-dom/test-utils";
import { CardSummaryBuilder } from "test/builders";
import { CardItem } from "../CardItem";

describe("CardItem", () => {
  const mockFunc = jest.fn((item: CardSummary) => {});

  beforeEach(() => {});

  afterEach(() => {
    mockFunc.mockClear();
  });

  it("should responce on onClick", async () => {
    let container = {} as HTMLElement;
    const card = new CardSummaryBuilder().build();
    act(() => {
      container = render(<CardItem card={card} onClick={(item) => mockFunc(item)} />).container;
    });

    expect(container).toBeTruthy();
    const submitButton = container.querySelector(".row-container") as HTMLElement;
    await act(async () => {
      submitButton.click();
    });
    expect(mockFunc).toHaveBeenCalledTimes(1);
    expect(mockFunc).toHaveBeenCalledWith(card);
  });
});
