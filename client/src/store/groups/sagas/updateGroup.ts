import { call, put, select, takeLatest } from "@redux-saga/core/effects";
import { requestFailed } from "store/root/actions";
import { selectUserId } from "store/user/selectors";
import { AddGroup, getGroupsSummary, GroupsActionEnum } from "../actions";
import * as api from "pages/groups/services/groupsApi";
import { ApiResponse } from "common/models/response";
import UpdateGroupRequest from "pages/groups/models/updateGroupRequest";

function* updateGroup({ group }: AddGroup) {
  const userId: string = yield select(selectUserId);

  const request = {
    userId,
    groupId: group.id,
    groupName: group.name,
    back: group.back,
    front: group.front,
  } as UpdateGroupRequest;
  const { data, error }: { data: ApiResponse<string>; error: any } = yield call(
    () => api.updateGroup(request)
  );
  yield put(data ? getGroupsSummary() : requestFailed(error));
}

export default function* updateGroupEffect() {
  yield takeLatest(GroupsActionEnum.UPDATE_GROUP, updateGroup);
}
