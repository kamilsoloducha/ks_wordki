import * as api from "api";
import { call, put, select } from "@redux-saga/core/effects";
import { selectFilter } from "../selectors";
import { CardSummary, Filter } from "pages/cardsSearch/models";
import { SagaIterator } from "redux-saga";
import { searchSuccess } from "../reducer";
import { all, takeEvery } from "redux-saga/effects";

export function* searchEffect(): SagaIterator {
  yield takeEvery("cardsSearch/search", searchWorker);
}

export function* searchWorker(): SagaIterator {
  const filter: Filter = yield select(selectFilter);
  const searchRequest: api.CardsSearchQuery = {
    searchingTerm: filter.searchingTerm,
    pageNumber: filter.pageNumber,
    pageSize: filter.pageSize,
    isTicked: filter.tickedOnly,
    searchingDrawers: [],
    lessonIncluded: filter.lessonIncluded,
  };

  const [cards, cardsCount]: [CardSummary[], number] = yield all([
    call(api.searchCards, searchRequest),
    call(api.searchCardsCount, searchRequest),
  ]);

  yield put(searchSuccess({ cards, cardsCount }));
}
