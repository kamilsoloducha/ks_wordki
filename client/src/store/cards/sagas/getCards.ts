import { call, put, select } from "@redux-saga/core/effects";
import { selectUserId } from "store/user/selectors";
import * as api from "api";
import { SagaIterator } from "redux-saga";
import { takeEvery } from "redux-saga/effects";
import { PayloadAction } from "@reduxjs/toolkit";
import { applyFilters, getCardsSuccess } from "../reducer";
import { GetCards } from "../action-payload";
import { CardSummary } from "pages/cards/models";

export function* getCardsWorker(action: PayloadAction<GetCards>): any {
  const cards: CardSummary[] = yield call(api.cardsSummary, action.payload.groupId);

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
      cards,
    })
  );
  yield put(applyFilters());
}

export function* getCardsEffect(): SagaIterator {
  yield takeEvery("cards/getCards", getCardsWorker);
}
