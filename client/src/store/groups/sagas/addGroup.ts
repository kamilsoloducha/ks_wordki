import { call, put, select, takeLatest } from "@redux-saga/core/effects";
import { requestFailed } from "store/root/actions";
import { selectUserId } from "store/user/selectors";
import { AddGroup, getGroupsSummary, GroupsActionEnum } from "../actions";
import * as api from "pages/groups/services/groupsApi";
import { ApiResponse } from "common/models/response";
import AddGroupRequest from "pages/groups/models/addGroupRequest";

function* addGroup({ group }: AddGroup) {
  const userId: string = yield select(selectUserId);

  const request = {
    userId,
    groupName: group.name,
    back: group.back,
    front: group.front,
  } as AddGroupRequest;
  const { data, error }: { data: ApiResponse<string>; error: any } = yield call(
    () => api.addGroup(request)
  );
  yield put(data ? getGroupsSummary() : requestFailed(error));
}

export default function* addGroupEffect() {
  yield takeLatest(GroupsActionEnum.ADD_GROUP, addGroup);
}
