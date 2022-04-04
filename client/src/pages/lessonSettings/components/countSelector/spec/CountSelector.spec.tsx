import { CardSummary } from "pages/cards/models";
import ReactDOM from "react-dom";
import { act } from "react-dom/test-utils";
import { CardSummaryBuilder } from "test/builders";
import { CountSelector } from "../CountSelector";

describe("CountSelector", () => {
  let container: HTMLDivElement;
  const onSelectedChangedMock = jest.fn((_: number) => {});

  beforeEach(() => {
    container = document.createElement("div");
    document.body.appendChild(container);
  });

  afterEach(() => {
    document.body.removeChild(container);
    container.remove();
    onSelectedChangedMock.mockClear();
  });

  it("should be created", async () => {
    act(() => {
      ReactDOM.render(
        <CountSelector selected={0} all={0} onSelectedChanged={onSelectedChangedMock} />,
        container
      );
    });

    expect(container.querySelector(".count-container")).toBeTruthy();
  });
});
