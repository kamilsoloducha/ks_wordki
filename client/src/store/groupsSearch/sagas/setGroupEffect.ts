import { put, take } from "@redux-saga/core/effects";
import { getCards } from "../reducer";

export function* setGroupEffect() {
  yield take("groupsSearch/setGroup");
  yield put(getCards());
}
