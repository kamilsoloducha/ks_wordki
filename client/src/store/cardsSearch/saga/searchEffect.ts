import * as actions from "../actions";
import * as api from "pages/cards/services/groupDetailsApi";
import { call, put, select, takeLatest } from "@redux-saga/core/effects";
import { selectUserId } from "store/user/selectors";
import { selectFilter } from "../selectors";
import { CardsSearchRequest } from "pages/cardsSearch/models/requests/cardsSearchRequest";
import { CardSummary, Filter } from "pages/cardsSearch/models";

function* search() {
  const userId: string = yield select(selectUserId);
  const filter: Filter = yield select(selectFilter);

  const searchRequest: CardsSearchRequest = {
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

  yield put(actions.searchSuccess(cards, cardsCount));
}

export function* searchEffect() {
  yield takeLatest(actions.CardsSearchActionEnum.SEARCH, search);
}
