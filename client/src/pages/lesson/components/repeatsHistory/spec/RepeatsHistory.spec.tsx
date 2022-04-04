import ReactDOM from "react-dom";
import { act } from "react-dom/test-utils";
import { RepeatHistory } from "../RepeatsHistory";

describe("RepeatsHistory", () => {
  let container: HTMLDivElement;
  const onHideMock = jest.fn(() => {});

  beforeEach(() => {
    container = document.createElement("div");
    document.body.appendChild(container);
  });

  afterEach(() => {
    document.body.removeChild(container);
    container.remove();
    onHideMock.mockClear();
  });

  it("should be created", async () => {
    act(() => {
      ReactDOM.render(<RepeatHistory visible={true} onHide={onHideMock} history={[]} />, container);
    });

    expect(document.querySelector(".p-dialog")).toBeTruthy();
  });
});
