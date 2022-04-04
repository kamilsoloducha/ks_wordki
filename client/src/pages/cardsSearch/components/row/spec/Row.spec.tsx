import { CardSummary, Side } from "pages/cardsSearch/models";
import ReactDOM from "react-dom";
import { act } from "react-dom/test-utils";
import { Row } from "../Row";

describe("Row", () => {
  let container: HTMLDivElement;

  beforeEach(() => {
    container = document.createElement("div");
    document.body.appendChild(container);
  });

  afterEach(() => {
    document.body.removeChild(container);
    container.remove();
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
      ReactDOM.render(<Row card={card} />, container);
    });

    expect(container.querySelector(".row-container")).toBeTruthy();
  });
});
