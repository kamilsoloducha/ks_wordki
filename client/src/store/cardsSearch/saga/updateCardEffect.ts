import { call, put, takeEvery } from "@redux-saga/core/effects";
import * as api from "api";
import { SagaIterator } from "redux-saga";
import { PayloadAction } from "@reduxjs/toolkit";
import { UpdateCard } from "../action-payloads";
import { search } from "../reducer";

export function* updateCardEffect(): SagaIterator {
  yield takeEvery("cardsSerach/updateCard", updateCardWorker);
}

export function* updateCardWorker(action: PayloadAction<UpdateCard>): SagaIterator {
  const request: api.UpdateCardRequest = {
    front: {
      value: action.payload.card.front.value,
      example: action.payload.card.front.example,
      isUsed: action.payload.card.front.isUsed,
      isTicked: action.payload.card.front.isTicked,
    },
    back: {
      value: action.payload.card.back.value,
      example: action.payload.card.back.example,
      isUsed: action.payload.card.back.isUsed,
      isTicked: action.payload.card.back.isTicked,
    },
    comment: "",
  };
  yield call(api.updateCard2, request);
  yield put(search());
}
