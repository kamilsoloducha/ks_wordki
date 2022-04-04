import ReactDOM from "react-dom";
import { act } from "react-dom/test-utils";
import GroupRow from "../GroupRow";

describe("GroupRow", () => {
  let container: HTMLDivElement;

  beforeEach(() => {
    container = document.createElement("div");
    document.body.appendChild(container);
  });

  afterEach(() => {
    document.body.removeChild(container);
    container.remove();
  });

  it("should be created", async () => {
    const groupSummary = {
      id: "groupId",
      name: "name",
      front: 1,
      back: 2,
      cardsCount: 2,
      cardsEnabled: 1,
    };
    act(() => {
      ReactDOM.render(<GroupRow groupSummary={groupSummary} />, container);
    });

    expect(container.querySelector(".group-row-container")).toBeTruthy();
  });
});
