import { SagaIterator } from "redux-saga";
import { put, takeLatest } from "redux-saga/effects";
import { search } from "../reducer";

export function* filterEffect(): SagaIterator {
  yield takeLatest(
    [
      "cardsSearch/filterReset",
      "cardsSearch/filterSetPagination",
      "cardsSearch/filterSetLessonIncluded",
      "cardsSearch/filterSetTickedOnly",
    ],
    filterWorker
  );
}

export function* filterWorker(): SagaIterator {
  yield put(search());
}
