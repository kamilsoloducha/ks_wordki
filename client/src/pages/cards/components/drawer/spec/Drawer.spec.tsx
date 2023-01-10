import { render } from "@testing-library/react";
import { act } from "react-dom/test-utils";
import Drawer from "../Drawer";

describe("Drawer", () => {
  let container: HTMLElement;
  const mockFunc = jest.fn(() => {});

  beforeEach(() => {
  });

  afterEach(() => {
    mockFunc.mockClear();
  });

  it("tet", async () => {
    act(() => {
      container = render(<Drawer isUsed={false} drawer={0} onClick={() => mockFunc()} />).container;
    });

    const submitButton = container.querySelector("div") as HTMLElement;
    await act(async () => {
      submitButton.click();
    });
    expect(mockFunc).toHaveBeenCalledTimes(1);
  });
});
