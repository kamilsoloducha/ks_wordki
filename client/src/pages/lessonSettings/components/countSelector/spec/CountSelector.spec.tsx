import { render } from "@testing-library/react";
import { CardSummary } from "pages/cards/models";
import ReactDOM from "react-dom";
import { act } from "react-dom/test-utils";
import { CardSummaryBuilder } from "test/builders";
import { CountSelector } from "../CountSelector";

describe("CountSelector", () => {
  const onSelectedChangedMock = jest.fn((_: number) => {});

  beforeEach(() => {
  });

  afterEach(() => {
    onSelectedChangedMock.mockClear();
  });

  it("should be created", async () => {
    let container = {} as HTMLElement;
    act(() => {
      container = render(
        <CountSelector selected={0} all={0} onSelectedChanged={onSelectedChangedMock} />,
      ).container;
    });

    expect(container.querySelector(".count-container")).toBeTruthy();
  });
});
