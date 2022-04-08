import { call, put, select, take, takeLatest } from "@redux-saga/core/effects";
import * as actions from "../actions";
import * as api from "api";
import { selectUserId } from "store/user/selectors";
import { SagaIterator } from "redux-saga";

export function* deleteCardEffect(): SagaIterator {
  const action: actions.DeleteCard = yield take(actions.CardsSearchActionEnum.DELETE_CARD);
  const userId: string = yield select(selectUserId);
  yield call(api.deleteCard, userId, action.groupId, action.cardId);
  yield put(actions.search());
}
