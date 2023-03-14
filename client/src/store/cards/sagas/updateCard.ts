import { call, put, select } from "@redux-saga/core/effects";
import { requestFailed } from "store/root/actions";
import * as api from "api";
import { SagaIterator } from "redux-saga";
import { takeEvery } from "redux-saga/effects";
import { PayloadAction } from "@reduxjs/toolkit";
import { UpdateCard } from "../action-payload";
import { updateCardSuccess } from "../reducer";

export function* updateCardWorker(action: PayloadAction<UpdateCard>): any {
  const response: {} | boolean = yield call(api.updateCard, action.payload.card);
  yield put(
    response !== false ? updateCardSuccess({ card: action.payload.card }) : requestFailed({} as any)
  );
}

export function* updateCardEffect(): SagaIterator {
  yield takeEvery("cards/updateCard", updateCardWorker);
}
