import * as redux from "react-redux";
import * as router from "react-router";
import ReactDOM from "react-dom";
import { act } from "react-dom/test-utils";
import { Router } from "react-router";
import history from "common/services/history";
import { ReactElement } from "react";
import configureStore from "redux-mock-store";
import { MainState } from "store/store";
import React from "react";
import GroupsPage from "../GroupsPage";

describe("GroupsPage", () => {
  let container: HTMLDivElement;
  let component: ReactElement;
  // const useDispatchMock = jest.spyOn(redux, "useDispatch");
  const dispatchMock = jest.fn(() => {});
  let mockStore: MainState;
  let store: any;

  beforeEach(() => {
    // dispatchMock.mockClear();

    // useDispatchMock.mockClear();
    // useDispatchMock.mockReturnValue(dispatchMock as any);

    container = document.createElement("div");
    document.body.appendChild(container);

    mockStore = {
      groupsReducer: {
        isLoading: true,
        groups: [],
        selectedItem: null,
        selectedItems: [],
        searchingGroups: [],
      },
    } as any;

    store = configureStore([])(mockStore);

    component = (
      <>
        <redux.Provider store={store}>
          <GroupsPage />
        </redux.Provider>
      </>
    );
  });

  afterEach(() => {
    document.body.removeChild(container);
    container.remove();
  });

  // it("should display spinner if it is loading", () => {
  //   act(() => {
  //     ReactDOM.render(component, container);
  //   });

  //   expect(container.querySelector(".loader")).toBeTruthy();
  // });
});
