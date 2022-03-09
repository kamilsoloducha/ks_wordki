import { put, takeLatest } from "redux-saga/effects";
import * as actions from "../actions";

function* search() {
  yield put(actions.search());
}

export function* setFilterEffect() {
  yield takeLatest(
    [
      actions.CardsSearchActionEnum.FILTER_SET_TERM,
      actions.CardsSearchActionEnum.FILTER_SET_PAGINATION,
    ],
    search
  );
}
