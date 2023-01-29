import { call, put, select } from "@redux-saga/core/effects";
import { selectUserId } from "store/user/selectors";
import * as api from "api";
import { SagaIterator } from "redux-saga";
import { takeEvery } from "redux-saga/effects";
import { PayloadAction } from "@reduxjs/toolkit";
import { applyFilters, getCardSuccess } from "../reducer";
import { GetCard } from "../action-payload";
import { CardSummary } from "pages/cards/models";

export function* getCardWorker(action: PayloadAction<GetCard>): any {
  const userId: string = yield select(selectUserId);
  const card: CardSummary = yield call(
    api.getCard,
    userId,
    action.payload.groupId,
    action.payload.cardId
  );

  yield put(getCardSuccess(card));
  yield put(applyFilters());
}

export function* getCardEffect(): SagaIterator {
  yield takeEvery("cards/getCard", getCardWorker);
}
