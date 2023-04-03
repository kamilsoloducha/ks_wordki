import { put, select, takeEvery } from "@redux-saga/core/effects";
import { FilterModel } from "../state";
import { selectCards, selectFilterState } from "../selectors";
import { CardSummary, SideSummary } from "pages/cards/models";
import { SagaIterator } from "redux-saga";
import { setFilteredCards } from "../reducer";

export function* filterCards(): SagaIterator {
  const filterState: FilterModel = yield select(selectFilterState);
  let cards: CardSummary[] = yield select(selectCards);
  if (filterState.drawer !== null) {
    cards = cards.filter((item) => isCardFromDrawer(item, Number(filterState.drawer)));
  }

  if (filterState.isLearning !== null) {
    const filterMethod = filterState.isLearning ? isCardInUsed : isCardNotInUsed;
    cards = cards.filter((item) => filterMethod(item));
  }

  if (filterState.isTicked) {
    cards = cards.filter((item) => {
      return item.front.isTicked || item.back.isTicked;
    });
  }

  if (filterState.text.length > 2) {
    cards = filterByText(String(filterState.text), cards);
  }
  yield put(setFilteredCards({ cards }));
}

function isCardFromDrawer(card: CardSummary, drawer: number): boolean {
  return isSideFromDrawer(card.front, drawer) || isSideFromDrawer(card.back, drawer);
}

function isSideFromDrawer(side: SideSummary, drawer: number): boolean {
  return side.drawer === drawer && side.isUsed;
}

function isCardInUsed(card: CardSummary): boolean {
  return card.front.isUsed || card.back.isUsed;
}

function isCardNotInUsed(card: CardSummary): boolean {
  return !card.front.isUsed || !card.back.isUsed;
}

function filterByText(text: string, cards: CardSummary[]): CardSummary[] {
  const searchValue = text.toLowerCase();
  return cards.filter(
    (item) =>
      item.front.value.toLowerCase().indexOf(searchValue) >= 0 ||
      item.back.value.toLowerCase().indexOf(searchValue) >= 0
  );
}

export function* setFilterEffect() {
  yield takeEvery(
    [
      "cards/setFilterDrawer",
      "cards/setFilterIsTicked",
      "cards/setFilterLearning",
      "cards/setFilterText",
      "cards/applyFilters",
      "cards/resetFilters",
    ],
    filterCards
  );
}
