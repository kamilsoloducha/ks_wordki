import { call, put, select } from "@redux-saga/core/effects";
import { requestFailed } from "store/root/actions";
import { selectUserId } from "store/user/selectors";
import { selectGroupId, selectSelectedCard } from "../selectors";
import * as api from "api";
import { CardSummary } from "pages/cards/models";
import { SagaIterator } from "redux-saga";
import { take, takeEvery } from "redux-saga/effects";
import { deleteCardSuccess } from "../reducer";

export function* deleteCardWorker():any{
  const userId: string = yield select(selectUserId);
  const groupId: string = yield select(selectGroupId);
  const selectedItem: CardSummary = yield select(selectSelectedCard);

  const response: {} | boolean = yield call(api.deleteCard, userId, groupId, selectedItem.id);
  yield put(
    response !== false ? deleteCardSuccess({ cardId: selectedItem.id }) : requestFailed({} as any)
  );
}

export function* deleteCardEffect(): SagaIterator {
    yield takeEvery("cards/deleteCard", deleteCardWorker);
}