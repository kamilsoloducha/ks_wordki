import { render } from "@testing-library/react";
import GroupsState from "store/groups/state";
import configureMockStore from 'redux-mock-store';
import { Provider } from "react-redux";
import GroupsPage from "../GroupsPage";


const mockedUsedNavigate = jest.fn();

jest.mock('react-router-dom', () => ({
  ...jest.requireActual('react-router-dom') as any,
  useNavigate: () => mockedUsedNavigate,
}));

describe("GroupsPage", () => {

  const mockState: GroupsState = {
    isLoading: false,
    groups: [],
    selectedItem: null,
    selectedItems: [],
    searchingGroups: []
  }

  const mockStore = configureMockStore([])({ groupsReducer: mockState });
  beforeEach(() => {
  });

  afterEach(() => {
    jest.clearAllMocks();
    mockStore.clearActions();
  });

  it("should be rendered", () => {
    mockState.isLoading = false;

    const { container } = render(
      <Provider store={mockStore}>
        <GroupsPage />
      </Provider>
    );

    expect(container.getElementsByClassName("groups-action-container").length).toBe(1);
  });

  it("should render spinner if isLoading", () => {
    mockState.isLoading = true;

    const { container } = render(
      <Provider store={mockStore}>
        <GroupsPage />
      </Provider>
    );

    expect(container.getElementsByClassName("loader").length).toBe(1);
  });

});
