import "test/matcher/toDeepEqual";
import * as actions from "../../actions";
import { SagaIterator } from "redux-saga";
import { put, select } from "redux-saga/effects";
import { selectCards, selectFilterState } from "store/cards/selectors";
import { CardSummary } from "pages/cards/models";
import { filterCards } from "../setFilter";
import { FilterModel } from "store/cards/state";

describe("filterCards", () => {
  let saga: SagaIterator;

  beforeEach(() => {
    saga = filterCards();
  });

  it("should go through", () => {
    const filterModel: FilterModel = {
      drawer: null,
      isLearning: null,
      text: "",
      isTicked: false,
    };
    const cards: CardSummary[] = [];
    expect(saga.next().value).toStrictEqual(select(selectFilterState));
    expect(saga.next(filterModel).value).toStrictEqual(select(selectCards));
    expect(saga.next(cards).value).toDeepEqual(put(actions.setFilteredCards([])));
  });
});
