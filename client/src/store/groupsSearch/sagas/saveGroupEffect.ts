import * as actions from "../actions";
import * as api from "pages/groupsSearch/services/groupsSearchApi";
import { call, put, select, takeLatest } from "@redux-saga/core/effects";
import { selectSelectedGroup } from "../selectors";
import { GroupSummary } from "pages/groupsSearch/models/groupSummary";
import { selectUserId } from "store/user/selectors";
import { SaveGroupRequest } from "pages/groupsSearch/models/requests/saveGroupRequest";

function* saveGroup() {
  const ownerId: string = yield select(selectUserId);
  const selectedGroup: GroupSummary = yield select(selectSelectedGroup);

  const request: SaveGroupRequest = {
    ownerId,
    groupId: selectedGroup.id,
  };

  yield call(api.saveGroup, request);

  yield put(actions.saveGroupSuccess());
  yield put(actions.resetSelection());
}

export function* saveGroupEffect() {
  yield takeLatest(actions.GroupsSearchActionEnum.SAVE_GROUP, saveGroup);
}
