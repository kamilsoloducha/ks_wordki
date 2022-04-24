import { SagaIterator } from "redux-saga";
import { debounce, put } from "redux-saga/effects";
import { search } from "../reducer";

export function* search2(): SagaIterator {
  yield put(search());
}

export function* setSearchingTermEffect(): SagaIterator {
  yield debounce(1000, "cardsSearch/filerSetTerm", search2);
}
