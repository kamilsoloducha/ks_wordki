import { call, put, takeEvery } from "@redux-saga/core/effects";
import * as api from "api";
import { Group } from "pages/lessonSettings/models/group";
import { SagaIterator } from "redux-saga";
import { getGroupsSuccess } from "../reducer";

export function* getGroupsEffect(): SagaIterator {
  yield takeEvery("lesson/getGroups", getGroup);
}

export function* getGroup(): SagaIterator {
  const groups: Group[] = yield call(api.getGroups);
  yield put(getGroupsSuccess({ groups }));
}
