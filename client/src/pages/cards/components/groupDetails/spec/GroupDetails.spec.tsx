import ReactDOM from "react-dom";
import { act } from "react-dom/test-utils";
import { GroupDetails } from "../GroupDetails";

describe("GroupDetails", () => {
  let container: HTMLDivElement;
  const mockFunc = jest.fn(() => {});

  beforeEach(() => {
    container = document.createElement("div");
    document.body.appendChild(container);
  });

  afterEach(() => {
    document.body.removeChild(container);
    container.remove();
    mockFunc.mockClear();
  });

  it("tet", async () => {
    act(() => {
      ReactDOM.render(
        <GroupDetails name="groupName" front={1} back={2} onSettingsClick={() => mockFunc()} />,
        container
      );
    });

    const submitButton = container.querySelector("img") as HTMLElement;
    await act(async () => {
      submitButton.click();
    });
    expect(mockFunc).toHaveBeenCalledTimes(1);
  });
});
