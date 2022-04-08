import { call, put, select, takeLatest } from "@redux-saga/core/effects";
import { requestFailed } from "store/root/actions";
import { selectUserId } from "store/user/selectors";
import { AddGroup, getGroupsSummary, GroupsActionEnum } from "../actions";
import * as api from "api";
import { ApiResponse } from "common/models/response";
import { take } from "redux-saga/effects";

export function* addGroupEffect() {
  const { group }: AddGroup = yield take(GroupsActionEnum.ADD_GROUP);
  const userId: string = yield select(selectUserId);

  const request = {
    userId,
    groupName: group.name,
    back: group.back,
    front: group.front,
  } as api.AddGroupRequest;
  const { data, error }: { data: ApiResponse<string>; error: any } = yield call(() =>
    api.addGroup(request)
  );
  yield put(data ? getGroupsSummary() : requestFailed(error));
}
