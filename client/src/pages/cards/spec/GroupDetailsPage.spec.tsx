import { act } from "react-dom/test-utils";
import GroupDetailsPage from "../GroupDetailsPage";
import { Provider } from "react-redux";
import CardsState, { FilterModel } from "store/cards/state";
import configureMockStore from "redux-mock-store";
import { render } from "@testing-library/react";
import { ReactElement } from "react";

const mockedUsedNavigate = jest.fn();

jest.mock("react-router-dom", () => ({
  ...(jest.requireActual("react-router-dom") as any),
  useNavigate: () => mockedUsedNavigate,
}));

describe("GroupDetailsPage", () => {
  const mockState: CardsState = {
    isLoading: false,
    id: "",
    name: "",
    language1: 0,
    language2: 0,
    cards: [],
    filteredCards: [],
    selectedItem: null,
    filter: {} as FilterModel,
  };

  const mockStore = configureMockStore([])({
    cardsReducer: mockState,
    lessonReducer: { languages: [] },
  });
  let component: ReactElement;

  beforeEach(() => {
    component = (
      <Provider store={mockStore}>
        <GroupDetailsPage />
      </Provider>
    );
  });

  afterEach(() => {
    jest.clearAllMocks();
    mockStore.clearActions();
  });

  it("should display spinner if it is loading", () => {
    mockState.isLoading = true;

    const { container } = render(component);

    expect(container.querySelector(".loader")).toBeTruthy();
  });

  it("should hide spinner if it is not loading", () => {
    mockState.isLoading = false;

    const { container } = render(component);

    expect(container.querySelector(".loader")).toBeNull();
  });

  it("should show action dialog after on settings click", async () => {
    mockState.isLoading = false;

    const { container } = render(component);

    const submitButton = container
      .querySelector(".group-details-container")
      ?.querySelector("img") as HTMLElement;
    await act(async () => {
      submitButton.click();
    });

    expect(document.querySelector(".actions-dialog-container")).toBeTruthy();
  });
});
