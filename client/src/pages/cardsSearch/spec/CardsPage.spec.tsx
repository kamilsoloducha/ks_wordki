import * as redux from "react-redux";
import { act } from "react-dom/test-utils";
import { ReactElement } from "react";
import configureMockStore, { MockStoreEnhanced } from "redux-mock-store";
import CardsPage from "../CardsPage";
import { CardsSearchState, initialCardsSearchState } from "store/cardsSearch/state";
import { render } from "@testing-library/react";

const mockedUsedNavigate = jest.fn();

jest.mock('react-router-dom', () => ({
  ...jest.requireActual('react-router-dom') as any,
  useNavigate: () => mockedUsedNavigate,
}));

describe("CardsPage", () => {
  let container: HTMLElement;
  let component: ReactElement;
  let mockStore: MockStoreEnhanced<any, any>;
  let mockState: CardsSearchState;

  beforeEach(() => {
    mockState = { ...initialCardsSearchState };

    mockStore = configureMockStore([])({ cardsSeachReducer: mockState });

    component = (
        <redux.Provider store={mockStore}>
          <CardsPage />
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
