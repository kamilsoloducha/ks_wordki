import { call, put, select, takeLatest } from "@redux-saga/core/effects";
import { requestFailed } from "store/root/actions";
import { selectUserId } from "store/user/selectors";
import { AppendCard, CardsActionEnum, getCards } from "../actions";
import { selectGroupId } from "../selectors";
import * as api from "api";
import { take } from "redux-saga/effects";
import { SagaIterator } from "redux-saga";

export function* appendCardEffect(): SagaIterator {
  const action: AppendCard = yield take(CardsActionEnum.APPEND_CARD);
  const userId: string = yield select(selectUserId);
  const id: string = yield select(selectGroupId);

  const { data, error }: { data: {}; error: any } = yield call(() =>
    api.appendCards(userId, id, action.count, action.languages)
  );
  yield put(data ? getCards(id) : requestFailed(error));
}
