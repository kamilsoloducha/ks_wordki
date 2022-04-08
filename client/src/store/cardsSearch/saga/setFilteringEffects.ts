import { SagaIterator } from "redux-saga";
import { put, takeLatest } from "redux-saga/effects";
import * as actions from "../actions";

export function* search(): SagaIterator {
  yield put(actions.search());
}

export function* setPaginationEffect(): SagaIterator {
  yield takeLatest(
    [
      actions.CardsSearchActionEnum.FILTER_RESET,
      actions.CardsSearchActionEnum.FILTER_SET_PAGINATION,
      actions.CardsSearchActionEnum.FILTER_SET_LESSON_INCLUDED,
      actions.CardsSearchActionEnum.FILTER_SET_TICKED_ONLY,
    ],
    search
  );
}
