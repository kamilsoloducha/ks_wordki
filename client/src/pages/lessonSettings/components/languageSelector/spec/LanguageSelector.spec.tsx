import { render } from "@testing-library/react";
import { CardSummary } from "pages/cards/models";
import ReactDOM from "react-dom";
import { act } from "react-dom/test-utils";
import { CardSummaryBuilder } from "test/builders";
import { LanguageSelector } from "../LanguageSelector";

describe("LanguageSelector", () => {
  const onSelectedChangedMock = jest.fn((_: number[]) => {});

  beforeEach(() => {
  });

  afterEach(() => {
    onSelectedChangedMock.mockClear();
  });

  it("should be created", async () => {
    let container = {} as HTMLElement;
    act(() => {
      container = render(
        <LanguageSelector selected={[]} onSelectedChanged={onSelectedChangedMock} />,
      ).container;
    });

    expect(container.querySelector(".language-container")).toBeTruthy();
  });
});
