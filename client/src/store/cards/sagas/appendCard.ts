import { call, put, select, takeLatest } from "@redux-saga/core/effects";
import { requestFailed } from "store/root/actions";
import { selectUserId } from "store/user/selectors";
import { AppendCard, CardsActionEnum, getCards } from "../actions";
import { selectGroupId } from "../selectors";
import * as api from "pages/cards/services/groupDetailsApi";

function* appendCards(action: AppendCard) {
  const userId: string = yield select(selectUserId);
  const id: number = yield select(selectGroupId);

  const { data, error }: { data: {}; error: any } = yield call(() =>
    api.appendCards(userId, id, action.count, action.languages)
  );
  yield put(data ? getCards(id) : requestFailed(error));
}

export function* appendCardEffect() {
  yield takeLatest(CardsActionEnum.APPEND_CARD, appendCards);
}
