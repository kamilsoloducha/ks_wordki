import { call, put, select, takeLatest } from "@redux-saga/core/effects";
import { requestFailed } from "store/root/actions";
import { selectUserId } from "store/user/selectors";
import { getGroupsSummary, GroupsActionEnum } from "../actions";
import * as api from "api";
import { ApiResponse } from "common/models/response";
import { selectSelectedItems } from "../selectors";

function* connectGroups() {
  const userId: string = yield select(selectUserId);
  const groupIds: string[] = yield select(selectSelectedItems);

  const request = {
    userId,
    groupIds,
  } as api.ConnectGroupsRequest;
  const { data, error }: { data: ApiResponse<string>; error: any } = yield call(() =>
    api.connectGroups(request)
  );
  yield put(data ? getGroupsSummary() : requestFailed(error));
}

export function* connectGroupsEffect() {
  yield takeLatest(GroupsActionEnum.CONNECT_GROUPS, connectGroups);
}
