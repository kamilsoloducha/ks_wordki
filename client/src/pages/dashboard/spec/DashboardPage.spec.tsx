import { render } from "@testing-library/react";
import { Provider } from "react-redux";
import DashboardPage from "../DashbaordPage";
import configureMockStore from "redux-mock-store";
import { getDashboardSummary } from "store/dashboard/reducer";
import DashboardState from "store/dashboard/state";
import { getLanguages } from "store/lesson/reducer";

const mockedUsedNavigate = jest.fn();

jest.mock("react-router-dom", () => ({
  ...(jest.requireActual("react-router-dom") as any),
  useNavigate: () => mockedUsedNavigate,
}));

describe("DashboardPage", () => {
  const mockState: DashboardState = {
    isLoading: false,
    dailyRepeats: 0,
    groupsCount: 0,
    cardsCount: 0,
    forecast: [],
    isForecastLoading: false,
  };

  const mockStore = configureMockStore([])({ dashboardReducer: mockState });

  afterEach(() => {
    jest.clearAllMocks();
    mockStore.clearActions();
  });

  it("should be rendered", () => {
    mockState.isLoading = false;

    const { container } = render(
      <Provider store={mockStore}>
        <DashboardPage />
      </Provider>
    );

    expect(container.getElementsByClassName("info-container").length).toBe(3);
  });

  [
    { index: 0, path: "/lesson-settings" },
    { index: 1, path: "/groups" },
    { index: 2, path: "/cards" },
  ].forEach((item, index) => {
    it("should be redirect proper page :: " + index, () => {
      mockState.isLoading = false;

      const { container } = render(
        <Provider store={mockStore}>
          <DashboardPage />
        </Provider>
      );
      const repeatInfo = container.getElementsByClassName("info-container")[
        item.index
      ] as HTMLElement;

      repeatInfo.click();

      expect(mockedUsedNavigate.mock.calls[0][0]).toBe(item.path);
    });
  });

  it("should dispatch action", () => {
    render(
      <Provider store={mockStore}>
        <DashboardPage />
      </Provider>
    );
    expect(mockStore.getActions().length).toBe(2);
    expect(mockStore.getActions()[0]).toStrictEqual(getDashboardSummary());
    expect(mockStore.getActions()[1]).toStrictEqual(getLanguages());
  });

  it("should display spinner if is loading", () => {
    mockState.isLoading = true;

    const { container } = render(
      <Provider store={mockStore}>
        <DashboardPage />
      </Provider>
    );
    expect(container.getElementsByClassName("loader").length).toBe(1);
  });
});
