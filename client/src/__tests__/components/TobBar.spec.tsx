import TopBar from "../../common/components/topBar/TopBar";
import { render, screen, fireEvent } from "@testing-library/react";
import { MemoryRouter } from "react-router-dom";
import "@testing-library/jest-dom";

const mockedUsedNavigate = jest.fn();

jest.mock("react-router-dom", () => ({
  ...(jest.requireActual("react-router-dom") as any),
  useNavigate: () => mockedUsedNavigate,
}));

describe("TobBar", () => {
  [true, false].forEach((item) => {
    it("should render Logo :: " + item, () => {
      const { container } = renderSut(item);
      const logoLink = container.querySelector(".top-bar-logo");
      expect(logoLink).toBeTruthy();
      expect(screen.getByText("Wordki").closest("a")).toHaveAttribute("href", "/dashboard");
    });
  });

  it("should display Logout if logged in", () => {
    renderSut(true);
    expect(screen.getByText("Logout").closest("a")).toHaveAttribute("href", "/logout");
  });

  it("should display Login and Register if logged out", () => {
    renderSut(false);
    expect(screen.getByText("Login").closest("a")).toHaveAttribute("href", "/login");
    expect(screen.getByText("Register").closest("a")).toHaveAttribute("href", "/register");
  });

  it("should render search input if logged in", () => {
    const { container } = renderSut(true);
    expect(container.querySelector("input")).toBeTruthy();
  });

  it("should not render search input if not logged in", () => {
    const { container } = renderSut(false);
    expect(container.querySelector("input")).toBeFalsy();
  });

  it("should navigate after search submit", () => {
    const { container } = renderSut(true);
    fireEvent.change(container.querySelector("input")!, { target: { value: "test" } });
    fireEvent.submit(container.querySelector("form")!);

    expect(mockedUsedNavigate).toHaveBeenCalledWith("/test?query=test&dic=Diki");
  });

  it("should not navigate if search is space", () => {
    const { container } = renderSut(true);
    fireEvent.change(container.querySelector("input")!, { target: { value: " " } });
    fireEvent.submit(container.querySelector("form")!);

    expect(mockedUsedNavigate).toHaveBeenCalledTimes(0);
  });
});

const sut = (isLogin: boolean) => (
  <>
    <MemoryRouter>
      <TopBar isLogin={isLogin}></TopBar>;
    </MemoryRouter>
  </>
);

const renderSut = (isLogin: boolean): { container: HTMLElement } => render(sut(isLogin));
