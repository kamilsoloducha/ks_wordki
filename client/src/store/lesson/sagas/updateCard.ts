import { call, put, select } from "@redux-saga/core/effects";
import { requestFailed } from "store/root/actions";
import { selectUserId } from "store/user/selectors";
import * as api from "api";
import { take } from "redux-saga/effects";
import { SagaIterator } from "redux-saga";
import { PayloadAction } from "@reduxjs/toolkit";
import { UpdateCard } from "../action-payloads";
import { updateCardSuccess } from "../reducer";

export function* updateCardEffect(): SagaIterator {
  while (true) {
    const action: PayloadAction<UpdateCard> = yield take("lesson/updateCard");

    const userId: string = yield select(selectUserId);

    const request = {
      front: {
        value: action.payload.form.frontValue,
        example: action.payload.form.frontExample,
        isUsed: action.payload.form.frontEnabled,
        isTicked: action.payload.form.isTicked,
      },
      back: {
        value: action.payload.form.backValue,
        example: action.payload.form.backExample,
        isUsed: action.payload.form.backEnabled,
        isTicked: action.payload.form.isTicked,
      },
      comment: "",
    } as api.UpdateCardRequest;

    const response: api.UpdateCardResponse | boolean = yield call(api.updateCard2, request);
    yield put(
      response !== false
        ? updateCardSuccess({ form: action.payload.form })
        : requestFailed({} as any)
    );
  }
}
