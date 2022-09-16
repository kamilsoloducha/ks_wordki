import { call, put, select } from "@redux-saga/core/effects";
import { selectUserId } from "store/user/selectors";
import * as api from "api";
import { SagaIterator } from "redux-saga";
import { takeEvery } from "redux-saga/effects";
import { PayloadAction } from "@reduxjs/toolkit";
import { applyFilters, getCardsSuccess } from "../reducer";
import { GetCards } from "../action-payload";

export function* getCardsWorker(action: PayloadAction<GetCards>):any{
  const userId: string = yield select(selectUserId);
  const cardsSummaryResponse: api.CardsSummaryResponse = yield call(
    api.cardsSummary,
    userId,
    action.payload.groupId
  );

  const groupDetailsResponse: api.GroupDetailsResponse = yield call(
    api.groupDetails,
    action.payload.groupId
  );

  yield put(
    getCardsSuccess({
      id: groupDetailsResponse.id,
      name: groupDetailsResponse.name,
      language1: groupDetailsResponse.front,
      language2: groupDetailsResponse.back,
      cards: cardsSummaryResponse.cards,
    })
  );
  yield put(applyFilters());
}

export function* getCardsEffect(): SagaIterator {
  yield takeEvery("cards/getCards", getCardsWorker);
}