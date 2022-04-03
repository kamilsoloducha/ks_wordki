import { call, put, select, takeLatest } from "@redux-saga/core/effects";
import { selectUserId } from "store/user/selectors";
import { applyFilters, CardsActionEnum, GetCards, getCardsSuccess } from "../actions";
import * as api from "api";

function* getCards(action: GetCards) {
  const userId: string = yield select(selectUserId);

  const cardsSummaryResponse: api.CardsSummaryResponse = yield call(
    async () => await api.cardsSummary(userId, action.groupId)
  );

  const groupDetailsResponse: api.GroupDetailsResponse = yield call(
    async () => await api.groupDetails(action.groupId)
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

export function* getCardsEffect() {
  yield takeLatest(CardsActionEnum.GET_CARDS, getCards);
}
