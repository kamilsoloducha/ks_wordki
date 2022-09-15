import * as redux from "react-redux";
import * as router from "react-router";
import ReactDOM from "react-dom";
import { act } from "react-dom/test-utils";
import GroupDetailsPage from "../GroupDetailsPage";
import { Router } from "react-router";
import { ReactElement } from "react";
import configureStore from "redux-mock-store";
import { MainState } from "store/store";
import React from "react";

describe("CardItem", () => {
  let container: HTMLDivElement;
  let component: ReactElement;
  const useDispatchMock = jest.spyOn(redux, "useDispatch");
  const useParamsMock = jest.spyOn(router, "useParams");
  const dispatchMock = jest.fn(() => {});
  let mockStore: MainState;
  let store: any;

  beforeEach(() => {
    dispatchMock.mockClear();

    useDispatchMock.mockClear();
    useDispatchMock.mockReturnValue(dispatchMock as any);

    useParamsMock.mockClear();
    useParamsMock.mockReturnValue({ groupId: "1" });

    container = document.createElement("div");
    document.body.appendChild(container);

    mockStore = {
      cardsReducer: {
        isLoading: false,
        id: "groupId",
        name: "test",
        language1: 1,
        language2: 2,
        cards: [],
        filteredCards: [],
        selectedItem: null,
        filter: {
          drawer: null,
          isLearning: null,
          text: "",
          isTicked: false,
        },
      },
    } as any;

    store = configureStore([])(mockStore);

    component = (
      <>
        <redux.Provider store={store}>
          {/* <Router history={history}>
            <GroupDetailsPage />
          </Router> */}
        </redux.Provider>
      </>
    );
  });

  afterEach(() => {
    document.body.removeChild(container);
    container.remove();
  });

  it("should display spinner if it is loading", () => {
    mockStore.cardsReducer.isLoading = true;
    act(() => {
      ReactDOM.render(component, container);
    });

    expect(container.querySelector(".loader")).toBeTruthy();
  });

  it("should hide spinner if it is not loading", () => {
    mockStore.cardsReducer.isLoading = false;
    act(() => {
      ReactDOM.render(component, container);
    });

    expect(container.querySelector(".loader")).toBeNull();
  });

  it("should show action dialog after on settings click", async () => {
    act(() => {
      ReactDOM.render(component, container);
    });
    const submitButton = container
      .querySelector(".group-details-container")
      ?.querySelector("img") as HTMLElement;
    await act(async () => {
      submitButton.click();
    });
    expect(document.querySelector(".actions-dialog-container")).toBeTruthy();
  });
});
