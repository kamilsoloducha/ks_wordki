import { call, put } from "@redux-saga/core/effects";
import * as api from "api";
import { SagaIterator } from "redux-saga";
import { takeEvery } from "redux-saga/effects";
import { PayloadAction } from "@reduxjs/toolkit";
import { applyFilters, getCardSuccess } from "../reducer";
import { GetCard } from "../action-payload";
import { CardSummary } from "pages/cards/models";

export function* getCardWorker(action: PayloadAction<GetCard>): any {
  const card: CardSummary = yield call(api.getCard, action.payload.cardId);

  yield put(getCardSuccess(card));
  yield put(applyFilters());
}

export function* getCardEffect(): SagaIterator {
  yield takeEvery("cards/getCard", getCardWorker);
}
