import { call, put, select } from "@redux-saga/core/effects";
import * as api from "api";
import { selectUserId } from "store/user/selectors";
import { ApiResponse } from "common/models/response";
import { SagaIterator } from "redux-saga";
import { take } from "redux-saga/effects";
import { getGroupsSuccess } from "../reducer";

export function* getGroupsEffect(): SagaIterator {
  yield take("lesson/getGroups");
  const ownerId: string = yield select(selectUserId);
  const request = { ownerId } as api.GetGroupsToLessonQuery;

  const apiResponse: ApiResponse<api.GetGroupToLessonResponse> = yield call(api.getGroups, request);
  yield put(getGroupsSuccess({ groups: apiResponse.response.groups }));
}
