import * as redux from "react-redux";
import ReactDOM from "react-dom";
import { act } from "react-dom/test-utils";
import { MainState } from "store/store";
import { Forecast } from "../Forecast";
import configureStore from "redux-mock-store";
import { ReactElement } from "react";

describe("Forecast", () => {
  let component: ReactElement;
  let container: HTMLDivElement;
  let mockStore: MainState;
  let store: any;

  beforeEach(() => {
    container = document.createElement("div");
    document.body.appendChild(container);

    mockStore = {
      dashboardReducer: {
        isLoading: true,
        dailyRepeats: 0,
        groupsCount: 0,
        cardsCount: 0,
        forecast: [],
        isForecastLoading: false,
      },
    } as any;

    store = configureStore([])(mockStore);

    component = (
      <>
        <redux.Provider store={store}>
          <Forecast />
        </redux.Provider>
      </>
    );
  });

  afterEach(() => {
    document.body.removeChild(container);
    container.remove();
  });

  it("should responce on onClick", async () => {
    act(() => {
      ReactDOM.render(component, container);
    });

    expect(container.querySelector(".forecast-container")).toBeTruthy();
  });
});
