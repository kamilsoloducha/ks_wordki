import { call, put, select, takeLatest } from "@redux-saga/core/effects";
import { DailyActionEnum, getGroupsSuccess } from "../actions";
import * as api from "pages/lesson/services/repeatsApi";
import { selectUserId } from "store/user/selectors";
import { ApiResponse } from "common/models/response";
import { GetGroupsToLessonRequest } from "pages/lesson/requests/getGroupsToLesson";
import { GetGroupToLessonResponse } from "pages/lesson/requests/responses/getGroupsToLessonResponse";

function* getGroups() {
  const ownerId: string = yield select(selectUserId);
  const request = { ownerId } as GetGroupsToLessonRequest;

  const apiResponse: ApiResponse<GetGroupToLessonResponse> = yield call(
    async () => await api.getGroups(request)
  );
  yield put(getGroupsSuccess(apiResponse.response.groups));
}

export default function* getGroupsEffect() {
  yield takeLatest(DailyActionEnum.GET_GROUPS, getGroups);
}
