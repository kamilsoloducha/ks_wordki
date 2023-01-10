import { render } from "@testing-library/react";
import ReactDOM from "react-dom";
import { act } from "react-dom/test-utils";
import { RepeatHistory } from "../RepeatsHistory";

describe("RepeatsHistory", () => {
  const onHideMock = jest.fn(() => {});

  beforeEach(() => {
  });

  afterEach(() => {
    onHideMock.mockClear();
  });

  it("should be created", async () => {
    let contianer = {} as HTMLElement;
    act(() => {
      contianer =  render(<RepeatHistory visible={true} onHide={onHideMock} history={[]} />).container;
    });

    expect(document.querySelector(".p-dialog")).toBeTruthy();
  });
});
