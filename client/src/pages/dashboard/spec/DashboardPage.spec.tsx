import { render } from "@testing-library/react";
import DashboardPage from "../DashbaordPage";
import * as redux from "react-redux";

const historyRecorder: any[] = [];
jest.mock("react-router-dom", () => ({
  ...jest.requireActual("react-router-dom"),
  useHistory: () => ({
    push: (val: any) => {
      historyRecorder.push(val);
    },
  }),
}));

describe("", () => {
  const useSelectorMock = jest.spyOn(redux, "useSelector");

  const useDispatchMock = jest.spyOn(redux, "useDispatch");
  const dispatchMock = jest.fn(() => {});

  beforeEach(() => {
    useSelectorMock.mockClear();

    useDispatchMock.mockClear();
    dispatchMock.mockClear();
    useDispatchMock.mockReturnValue(dispatchMock as any);

    historyRecorder.splice(0, historyRecorder.length);
  });

  it("should be rendered", () => {
    useSelectorMock.mockReturnValueOnce({
      dailyRepeats: 1,
      groupsCount: 1,
      cardsCount: 1,
      isLoading: false,
    });
    const { container } = render(<DashboardPage />);

    expect(container.getElementsByClassName("info-container").length).toBe(3);
  });

  [
    { index: 0, path: "/lesson-settings" },
    { index: 1, path: "/groups" },
    { index: 2, path: "/cards" },
  ].forEach((item, index) => {
    it("should be redirect proper page :: " + index, () => {
      useSelectorMock.mockReturnValueOnce({
        dailyRepeats: 1,
        groupsCount: 1,
        cardsCount: 1,
        isLoading: false,
      });
      const { container } = render(<DashboardPage />);
      const repeatInfo = container.getElementsByClassName("info-container")[
        item.index
      ] as HTMLElement;

      repeatInfo.click();
      expect(historyRecorder[0]).toBe(item.path);
    });
  });

  it("should dispatch action", () => {
    useSelectorMock.mockReturnValueOnce({
      dailyRepeats: 1,
      groupsCount: 1,
      cardsCount: 1,
      isLoading: false,
    });
    const { container } = render(<DashboardPage />);
    expect(dispatchMock).toHaveBeenCalledTimes(1);
    // expect(dispatchMock).toBeCalledWith(getDashboardSummary());
  });

  it("should display spinner if is loading", () => {
    useSelectorMock.mockReturnValueOnce({
      isLoading: true,
    });
    const { container } = render(<DashboardPage />);
    expect(container.getElementsByClassName("loader").length).toBe(1);
  });

  afterAll(() => {
    jest.clearAllMocks();
  });
});
