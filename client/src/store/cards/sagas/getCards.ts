import { call, put, select, takeLatest } from "@redux-saga/core/effects";
import { selectUserId } from "store/user/selectors";
import { applyFilters, CardsActionEnum, GetCards, getCardsSuccess } from "../actions";
import * as api from "api";
import { SagaIterator } from "redux-saga";
import { take } from "redux-saga/effects";

export function* getCardsEffect(): SagaIterator {
  const action: GetCards = yield take(CardsActionEnum.GET_CARDS);

  const userId: string = yield select(selectUserId);

  const cardsSummaryResponse: api.CardsSummaryResponse = yield call(
    api.cardsSummary,
    userId,
    action.groupId
  );

  const groupDetailsResponse: api.GroupDetailsResponse = yield call(
    api.groupDetails,
    action.groupId
  );

  yield put(
    getCardsSuccess(
      groupDetailsResponse.id,
      groupDetailsResponse.name,
      groupDetailsResponse.front,
      groupDetailsResponse.back,
      cardsSummaryResponse.cards
    )
  );
  yield put(applyFilters());
}
