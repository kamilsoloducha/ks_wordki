import * as api from "api";
import { call, put, select, take } from "@redux-saga/core/effects";
import { selectUserId } from "store/user/selectors";
import { selectFilter } from "../selectors";
import { CardSummary, Filter } from "pages/cardsSearch/models";
import { SagaIterator } from "redux-saga";
import { searchSuccess } from "../reducer";

export function* searchEffect(): SagaIterator {
  while (true) {
    yield take("cardsSearch/search");

    const userId: string = yield select(selectUserId);
    const filter: Filter = yield select(selectFilter);

    const searchRequest: api.CardsSearchQuery = {
      ownerId: userId,
      searchingTerm: filter.searchingTerm,
      pageNumber: filter.pageNumber,
      pageSize: filter.pageSize,
      onlyTicked: filter.tickedOnly ?? false,
      searchingDrawers: [],
      lessonIncluded: filter.lessonIncluded,
    };

    const cards: CardSummary[] = yield call(api.searchCards, searchRequest);
    const cardsCount: number = yield call(api.searchCardsCount, searchRequest);

    yield put(searchSuccess({ cards, cardsCount }));
  }
}