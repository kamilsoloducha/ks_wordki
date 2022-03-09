import * as actions from "../actions";
import * as api from "pages/cards/services/groupDetailsApi";
import { call, put, select, takeLatest } from "@redux-saga/core/effects";
import { selectUserId } from "store/user/selectors";
import { Filter } from "pages/cards/models/filter";
import { selectFilter } from "../selectors";
import { CardsSearchRequest } from "pages/cards/models/requests/cardsSearchRequest";
import { CardSummary } from "pages/cards/models/cardSummary";

function* search() {
  const userId: string = yield select(selectUserId);
  const filter: Filter = yield select(selectFilter);

  const searchRequest: CardsSearchRequest = {
    ownerId: userId,
    searchingTerm: filter.searchingTerm,
    pageNumber: filter.pageNumber,
    pageSize: filter.pageSize,
    onlyTicked: false,
    searchingDrawers: [],
    lessonIncluded: null,
  };

  const cards: CardSummary[] = yield call(api.searchCards, searchRequest);

  yield put(actions.searchSuccess(cards));
}

export function* searchEffect() {
  yield takeLatest(actions.CardsSearchActionEnum.SEARCH, search);
}
