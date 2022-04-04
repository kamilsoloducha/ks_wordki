import ReactDOM from "react-dom";
import { act } from "react-dom/test-utils";
import Drawer from "../Drawer";

describe("Drawer", () => {
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
      ReactDOM.render(<Drawer isUsed={false} drawer={0} onClick={() => mockFunc()} />, container);
    });

    const submitButton = container.querySelector("div") as HTMLElement;
    await act(async () => {
      submitButton.click();
    });
    expect(mockFunc).toHaveBeenCalledTimes(1);
  });
});
