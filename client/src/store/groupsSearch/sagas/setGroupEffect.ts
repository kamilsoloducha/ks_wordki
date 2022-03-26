import * as actions from "../actions";
import { put, takeLatest } from "@redux-saga/core/effects";

function* setGroup() {
  yield put(actions.getCards());
}

export function* setGroupEffect() {
  yield takeLatest(actions.GroupsSearchActionEnum.SET_GROUP, setGroup);
}
