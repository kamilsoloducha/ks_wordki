import { render } from "@testing-library/react";
import ReactDOM from "react-dom";
import { act } from "react-dom/test-utils";
import { GroupDetails } from "../GroupDetails";

describe("GroupDetails", () => {
  const mockFunc = jest.fn(() => {});

  beforeEach(() => {
  });

  afterEach(() => {
    mockFunc.mockClear();
  });

  it("tet", async () => {
    let container = {} as HTMLElement;
    act(() => {
      container = render(
        <GroupDetails name="groupName" front={1} back={2} onSettingsClick={() => mockFunc()} />,
      ).container;
    });

    const submitButton = container.querySelector("img") as HTMLElement;
    await act(async () => {
      submitButton.click();
    });
    expect(mockFunc).toHaveBeenCalledTimes(1);
  });
});
