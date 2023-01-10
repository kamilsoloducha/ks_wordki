import { render } from "@testing-library/react";
import ReactDOM from "react-dom";
import { act } from "react-dom/test-utils";
import GroupRow from "../GroupRow";

describe("GroupRow", () => {
  beforeEach(() => {
  });

  afterEach(() => {
  });

  it("should be created", async () => {
    let container = {} as HTMLElement;
    const groupSummary = {
      id: "groupId",
      name: "name",
      front: 1,
      back: 2,
      cardsCount: 2,
      cardsEnabled: 1,
    };
    act(() => {
      container = render(<GroupRow groupSummary={groupSummary} />).container;
    });

    expect(container.querySelector(".group-row-container")).toBeTruthy();
  });
});
