import { SagaIterator } from "redux-saga";
import { put, take } from "redux-saga/effects";
import { search } from "../reducer";

export function* setPaginationEffect(): SagaIterator {
  yield take([
    "cardsSerach/filterReset",
    "cardsSerach/filterSetPagination",
    "cardsSerach/filterSetLessonIncluded",
    "cardsSerach/filterSetTickedOnly",
  ]);
  yield put(search());
}
