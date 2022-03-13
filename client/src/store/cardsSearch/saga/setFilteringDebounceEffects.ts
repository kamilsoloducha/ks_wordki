import { debounce, put } from "redux-saga/effects";
import * as actions from "../actions";

function* search() {
  yield put(actions.search());
}

export function* setSearchingTermEffect() {
  yield debounce(1000, actions.CardsSearchActionEnum.FILTER_SET_TERM, search);
}
