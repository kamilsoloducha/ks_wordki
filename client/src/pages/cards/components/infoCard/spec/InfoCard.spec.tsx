import { render } from "@testing-library/react";
import { act } from "react-dom/test-utils";
import InfoCard from "../InfoCard";

let container: HTMLDivElement;

beforeEach(() => {
  container = document.createElement("div");
  document.body.appendChild(container);
});

afterEach(() => {
  document.body.removeChild(container);
  container.remove();
});

it("tet", () => {
  let container;
  act(() => {
    container = render(<InfoCard value={"test"} label={"test"} />).container;
  });
  expect(container).toBeTruthy();
});
