import { call, put, select, takeLatest } from "@redux-saga/core/effects";
import { GroupDetailsResponse } from "pages/cards/models/groupDetailsSummary";
import { selectUserId } from "store/user/selectors";
import { applyFilters, CardsActionEnum, GetCards, getCardsSuccess } from "../actions";
import * as api from "pages/cards/services/groupDetailsApi";
import { CardsSummaryResponse } from "pages/cards/models/cardsSummaryResponse";

function* getCards(action: GetCards) {
  const userId: string = yield select(selectUserId);

  const cardsSummaryResponse: CardsSummaryResponse = yield call(
    async () => await api.cardsSummary(userId, action.groupId)
  );

  const groupDetailsResponse: GroupDetailsResponse = yield call(
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
