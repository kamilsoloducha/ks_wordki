import * as actions from "../actions";
import { put, take } from "@redux-saga/core/effects";

export function* setGroupEffect() {
  yield take(actions.GroupsSearchActionEnum.SET_GROUP);
  yield put(actions.getCards());
}
