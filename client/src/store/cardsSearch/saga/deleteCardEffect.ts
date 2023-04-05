import { call, put, select, take } from "@redux-saga/core/effects";
import * as api from "api";
import { SagaIterator } from "redux-saga";
import { search } from "../reducer";
import { PayloadAction } from "@reduxjs/toolkit";
import { DeleteCard } from "../action-payloads";

export function* deleteCardEffect(): SagaIterator {
  while (true) {
    const action: PayloadAction<DeleteCard> = yield take("cardsSearch/deleteCard");
    yield call(api.deleteCard, action.payload.cardId);
    yield put(search());
  }
}
