import { render } from "@testing-library/react";
import { CardSummary, Side } from "pages/cardsSearch/models";
import ReactDOM from "react-dom";
import { act } from "react-dom/test-utils";
import { Row } from "../Row";

describe("Row", () => {
  let container: HTMLElement;

  beforeEach(() => {
  });

  afterEach(() => {
  });

  it("should responce on onClick", async () => {
    const card: CardSummary = {
      id: "id",
      groupName: "groupName",
      groupId: "groupId",
      front: {} as Side,
      back: {} as Side,
    };
    act(() => {
      container = render(<Row card={card} />).container;
    });

    expect(container.querySelector(".row-container")).toBeTruthy();
  });
});
