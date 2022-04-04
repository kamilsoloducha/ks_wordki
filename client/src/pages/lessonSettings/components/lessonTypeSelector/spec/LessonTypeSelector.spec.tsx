import ReactDOM from "react-dom";
import { act } from "react-dom/test-utils";
import { LessonTypeSelector } from "../LessonTypeSelector";

describe("LessonTypeSelector", () => {
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
        <LessonTypeSelector selected={0} onSelectedChanged={onSelectedChangedMock} />,
        container
      );
    });

    expect(container.querySelector(".lesson-type-container")).toBeTruthy();
  });
});
