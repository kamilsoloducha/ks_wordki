import { call, put, select, takeLatest } from "@redux-saga/core/effects";
import { requestFailed } from "store/root/actions";
import { selectUserId } from "store/user/selectors";
import * as api from "api";
import { DailyActionEnum, UpdateCard, updateCardSuccess } from "../actions";
import { take } from "redux-saga/effects";
import { SagaIterator } from "redux-saga";

export function* updateCardEffect(): SagaIterator {
  const action: UpdateCard = yield take(DailyActionEnum.UPDATE_CARD);

  const userId: string = yield select(selectUserId);

  const request = {
    userId,
    groupId: action.groupId,
    cardId: action.form.cardId,
    front: {
      value: action.form.frontValue,
      example: action.form.frontExample,
      isUsed: action.form.frontEnabled,
      isTicked: action.form.isTicked,
    },
    back: {
      value: action.form.backValue,
      example: action.form.backExample,
      isUsed: action.form.backEnabled,
      isTicked: action.form.isTicked,
    },
  } as api.UpdateCardRequest;

  const response: api.UpdateCardResponse | boolean = yield call(api.updateCard2, request);
  yield put(response !== false ? updateCardSuccess(action.form) : requestFailed({} as any));
}
