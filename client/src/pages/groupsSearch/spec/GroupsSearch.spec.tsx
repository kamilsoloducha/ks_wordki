import * as redux from "react-redux";
import { act } from "react-dom/test-utils";
import { ReactElement } from "react";
import configureMockStore, { MockStoreEnhanced } from "redux-mock-store";
import GroupsSearchPage from "../GroupsSearch";
import { GroupsSearchState } from "store/groupsSearch/state";
import { render } from "@testing-library/react";

const mockedUsedNavigate = jest.fn();

jest.mock('react-router-dom', () => ({
  ...jest.requireActual('react-router-dom') as any,
  useNavigate: () => mockedUsedNavigate,
}));

describe("GroupsPage", () => {
  let container: HTMLElement;
  let component: ReactElement;
  let mockState: GroupsSearchState;
  let mockStore: MockStoreEnhanced<any, any>;

  beforeEach(() => {
    mockState = {
      isSearching: false,

        groups: [],
        groupsCount: 0,

        selectedGroup: null,
        isCardsLoading: false,
        cards: [],

        groupName: "",
    }

    mockStore = configureMockStore([])({ groupsSearchReducer: mockState });

    component = (
        <redux.Provider store={mockStore}>
          <GroupsSearchPage />
        </redux.Provider>
    );
  });

  afterEach(() => {
    mockStore.clearActions();
    jest.clearAllMocks();
  });

  it("should display spinner if it is loading", () => {
    mockState.isSearching = true;

    act(() => {
      container = render(component).container;
    });

    expect(container.querySelector(".loader")).toBeTruthy();
  });
});
