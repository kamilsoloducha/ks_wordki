import { call, put, select, take } from "@redux-saga/core/effects";
import * as api from "api";
import { selectUserId } from "store/user/selectors";
import { SagaIterator } from "redux-saga";
import { search } from "../reducer";
import { PayloadAction } from "@reduxjs/toolkit";
import { DeleteCard } from "../action-payloads";

export function* deleteCardEffect(): SagaIterator {
  const action: PayloadAction<DeleteCard> = yield take("cardsSearch/deleteCard");
  const userId: string = yield select(selectUserId);
  yield call(api.deleteCard, userId, action.payload.groupId, action.payload.cardId);
  yield put(search());
}
