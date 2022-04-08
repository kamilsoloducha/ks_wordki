import { call, put, select, takeLatest } from "@redux-saga/core/effects";
import { requestFailed } from "store/root/actions";
import { selectUserId } from "store/user/selectors";
import { CardsActionEnum, UpdateCard, updateCardSuccess } from "../actions";
import { selectGroupId } from "../selectors";
import * as api from "api";
import { SagaIterator } from "redux-saga";
import { take } from "redux-saga/effects";

export function* updateCardEffect(): SagaIterator {
  const action: UpdateCard = yield take(CardsActionEnum.UPDATE_CARD);

  const userId: string = yield select(selectUserId);
  const id: string = yield select(selectGroupId);

  const { data, error }: { data: {}; error: any } = yield call(() =>
    api.updateCard(userId, id, action.card)
  );
  yield put(data ? updateCardSuccess(action.card) : requestFailed(error));
}
