import { call, put, select } from "@redux-saga/core/effects";
import { requestFailed } from "store/root/actions";
import { selectUserId } from "store/user/selectors";
import { selectGroupId } from "../selectors";
import { addCard } from "api";
import { SagaIterator } from "redux-saga";
import { take } from "redux-saga/effects";
import { PayloadAction } from "@reduxjs/toolkit";
import { UpdateCard } from "../action-payload";
import { selectCard } from "../reducer";
import { CardSummary } from "pages/cards/models";

export function* addCardEffect(): SagaIterator {
  const action: PayloadAction<UpdateCard> = yield take("cards/addCard");
  const userId: string = yield select(selectUserId);
  const id: string = yield select(selectGroupId);

  const response: string | boolean = yield call(addCard, userId, id, action.payload.card);
  yield put(
    response !== false ? selectCard({ item: {} as CardSummary }) : requestFailed({} as any)
  );
}
