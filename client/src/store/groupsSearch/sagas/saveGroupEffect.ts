import * as actions from "../actions";
import * as api from "api";
import { call, put, select, take, takeLatest } from "@redux-saga/core/effects";
import { selectSelectedGroup } from "../selectors";
import { GroupSummary } from "pages/groupsSearch/models/groupSummary";
import { selectUserId } from "store/user/selectors";
import { SagaIterator } from "redux-saga";

export function* saveGroupEffect(): SagaIterator {
  yield take(actions.GroupsSearchActionEnum.SAVE_GROUP);
  const ownerId: string = yield select(selectUserId);
  const selectedGroup: GroupSummary = yield select(selectSelectedGroup);

  const request: api.SaveGroupRequest = {
    ownerId,
    groupId: selectedGroup.id,
  };

  yield call(api.saveGroup, request);

  yield put(actions.saveGroupSuccess());
  yield put(actions.resetSelection());
}
