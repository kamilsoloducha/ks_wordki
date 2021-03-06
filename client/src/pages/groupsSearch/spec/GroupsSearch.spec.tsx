import * as redux from "react-redux";
import ReactDOM from "react-dom";
import { act } from "react-dom/test-utils";
import { ReactElement } from "react";
import configureStore from "redux-mock-store";
import { MainState } from "store/store";
import GroupsSearchPage from "../GroupsSearch";

describe("GroupsPage", () => {
  let container: HTMLDivElement;
  let component: ReactElement;
  const useDispatchMock = jest.spyOn(redux, "useDispatch");
  const dispatchMock = jest.fn(() => {});
  let mockStore: MainState;
  let store: any;

  beforeEach(() => {
    dispatchMock.mockClear();

    useDispatchMock.mockClear();
    useDispatchMock.mockReturnValue(dispatchMock as any);

    container = document.createElement("div");
    document.body.appendChild(container);

    mockStore = {
      groupsSearchReducer: {
        isSearching: false,

        groups: [],
        groupsCount: 0,

        selectedGroup: null,
        isCardsLoading: false,
        cards: [],

        groupName: "",
      },
    } as any;

    store = configureStore([])(mockStore);

    component = (
      <>
        <redux.Provider store={store}>
          <GroupsSearchPage />
        </redux.Provider>
      </>
    );
  });

  afterEach(() => {
    document.body.removeChild(container);
    container.remove();
  });

  it("should display spinner if it is loading", () => {
    mockStore.groupsSearchReducer.isSearching = true;
    act(() => {
      ReactDOM.render(component, container);
    });

    expect(container.querySelector(".loader")).toBeTruthy();
  });
});
