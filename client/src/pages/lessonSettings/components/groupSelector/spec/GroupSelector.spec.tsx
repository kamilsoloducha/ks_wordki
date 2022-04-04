import { CardSummary } from "pages/cards/models";
import ReactDOM from "react-dom";
import { act } from "react-dom/test-utils";
import { CardSummaryBuilder } from "test/builders";
import { GroupSelector } from "../GroupSelector";

describe("GroupSelector", () => {
  let container: HTMLDivElement;
  const onSelectedChangedMock = jest.fn((_: string) => {});

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
        <GroupSelector items={[]} selectedGroupId={""} onSelectedChanged={onSelectedChangedMock} />,
        container
      );
    });

    expect(container.querySelector(".group-selector-container")).toBeTruthy();
  });
});
