import { call, put, select, takeLatest } from "@redux-saga/core/effects";
import GroupDetailsSummary from "pages/cards/models/groupDetailsSummary";
import { requestFailed } from "store/root/actions";
import { selectUserId } from "store/user/selectors";
import { CardsActionEnum, GetCards, getCardsSuccess } from "../actions";
import * as api from "pages/cards/services/groupDetailsApi";

function* getCards(action: GetCards) {
  const userId: string = yield select(selectUserId);
  const { data, error }: { data: GroupDetailsSummary; error: any } = yield call(
    () => api.groupDetails(userId, action.groupId)
  );
  yield put(
    data
      ? getCardsSuccess(data.id, data.name, data.front, data.back, data.cards)
      : requestFailed(error)
  );
}

export default function* getCardsEffect() {
  yield takeLatest(CardsActionEnum.GET_CARDS, getCards);
}
