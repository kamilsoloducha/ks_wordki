import { put, takeLatest } from "redux-saga/effects";
import * as actions from "../actions";

function* search() {
  yield put(actions.search());
}

export function* setPaginationEffect() {
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
