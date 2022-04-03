import { call, put, select, takeLatest } from "@redux-saga/core/effects";
import * as actions from "../actions";
import * as api from "api";
import { selectUserId } from "store/user/selectors";

function* deleteCard(action: actions.DeleteCard) {
  const userId: string = yield select(selectUserId);
  yield call(api.deleteCard, userId, action.groupId, action.cardId);

  yield put(actions.search());
}

export function* deleteCardEffect() {
  yield takeLatest(actions.CardsSearchActionEnum.DELETE_CARD, deleteCard);
}
