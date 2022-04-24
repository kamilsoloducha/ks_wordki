import { call, put, select } from "@redux-saga/core/effects";
import { requestFailed } from "store/root/actions";
import { selectUserId } from "store/user/selectors";
import { selectGroupId } from "../selectors";
import * as api from "api";
import { SagaIterator } from "redux-saga";
import { take } from "redux-saga/effects";
import { PayloadAction } from "@reduxjs/toolkit";
import { UpdateCard } from "../action-payload";
import { updateCardSuccess } from "../reducer";

export function* updateCardEffect(): SagaIterator {
  const action: PayloadAction<UpdateCard> = yield take("cards/updateCard");

  const userId: string = yield select(selectUserId);
  const id: string = yield select(selectGroupId);

  const response: {} | boolean = yield call(api.updateCard, userId, id, action.payload.card);
  yield put(
    response !== false ? updateCardSuccess({ card: action.payload.card }) : requestFailed({} as any)
  );
}
