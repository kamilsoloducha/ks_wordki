import { call, put, select } from "@redux-saga/core/effects";
import { DailyActionEnum, getGroupsSuccess } from "../actions";
import * as api from "api";
import { selectUserId } from "store/user/selectors";
import { ApiResponse } from "common/models/response";
import { SagaIterator } from "redux-saga";
import { take } from "redux-saga/effects";

export function* getGroupsEffect(): SagaIterator {
  yield take(DailyActionEnum.GET_GROUPS);
  const ownerId: string = yield select(selectUserId);
  const request = { ownerId } as api.GetGroupsToLessonQuery;

  const apiResponse: ApiResponse<api.GetGroupToLessonResponse> = yield call(api.getGroups, request);
  yield put(getGroupsSuccess(apiResponse.response.groups));
}
