import { render } from "@testing-library/react";
import ReactDOM from "react-dom";
import { act } from "react-dom/test-utils";
import { LessonTypeSelector } from "../LessonTypeSelector";

describe("LessonTypeSelector", () => {
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
        <LessonTypeSelector selected={0} onSelectedChanged={onSelectedChangedMock} />
      ).container;
    });

    expect(container.querySelector(".lesson-type-container")).toBeTruthy();
  });
});
