import { act, render } from "@testing-library/react";
import { Info } from "./Info";

describe("Info", () => {
  it("should be renderd", () => {
    const onClick = jest.fn();
    const { container } = render(<Info title="InfoTitle" value={20} onClick={onClick} />);

    expect(container.innerHTML).toContain("InfoTitle");
    expect(container.innerHTML).toContain("20");

    var onClickContainer = container.getElementsByClassName("info-container")[0] as HTMLElement;

    act(() => {
      onClickContainer.click();
    });

    expect(onClick.mock.calls.length).toBe(1);
  });

  it("should react on click", () => {
    const onClick = jest.fn();
    const { container } = render(<Info title="InfoTitle" value={20} onClick={onClick} />);

    var onClickContainer = container.getElementsByClassName("info-container")[0] as HTMLElement;
    onClickContainer.click();

    expect(onClick).toHaveBeenCalledTimes(1);
  });
});
