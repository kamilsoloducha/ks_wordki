import { SagaIterator } from "redux-saga";
import { debounce, put } from "redux-saga/effects";
import * as actions from "../actions";

export function* search(): SagaIterator {
  yield put(actions.search());
}

export function* setSearchingTermEffect(): SagaIterator {
  yield debounce(1000, actions.CardsSearchActionEnum.FILTER_SET_TERM, search);
}
