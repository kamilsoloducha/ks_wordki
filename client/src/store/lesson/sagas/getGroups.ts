import { call, put, select, takeLatest } from "@redux-saga/core/effects";
import { DailyActionEnum, getGroupsSuccess } from "../actions";
import * as api from "api";
import { selectUserId } from "store/user/selectors";
import { ApiResponse } from "common/models/response";

function* getGroups() {
  const ownerId: string = yield select(selectUserId);
  const request = { ownerId } as api.GetGroupsToLessonQuery;

  const apiResponse: ApiResponse<api.GetGroupToLessonResponse> = yield call(
    async () => await api.getGroups(request)
  );
  yield put(getGroupsSuccess(apiResponse.response.groups));
}

export function* getGroupsEffect() {
  yield takeLatest(DailyActionEnum.GET_GROUPS, getGroups);
}
