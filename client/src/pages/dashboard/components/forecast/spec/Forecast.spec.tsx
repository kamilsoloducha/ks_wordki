import * as redux from "react-redux";
import ReactDOM from "react-dom";
import { act } from "react-dom/test-utils";
import { MainState } from "store/store";
import { Forecast } from "../Forecast";
import configureStore from "redux-mock-store";
import { ReactElement } from "react";
import { render } from "@testing-library/react";

describe("Forecast", () => {
  let component: ReactElement;
  let mockStore: MainState;
  let store: any;

  beforeEach(() => {
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
  });

  it("should responce on onClick", async () => {
    let container = {} as HTMLElement;
    act(() => {
     container =  render(component).container;
    });

    expect(container.querySelector(".forecast-container")).toBeTruthy();
  });
});
