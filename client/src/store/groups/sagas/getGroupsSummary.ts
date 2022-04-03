import { call, put, takeLatest, select } from "@redux-saga/core/effects";
import { requestFailed } from "store/root/actions";
import { selectUserId } from "store/user/selectors";
import { GetGroupsSummary, getGroupsSummarySuccess, GroupsActionEnum } from "../actions";
import * as api from "api";

function* getGroupsSummary(_: GetGroupsSummary) {
  const userId: string = yield select(selectUserId);
  const { data, error }: { data: api.GroupsSummaryResponse; error: any } = yield call(() =>
    api.groups(userId)
  );
  yield put(data ? getGroupsSummarySuccess(data.groups) : requestFailed(error));
}

export function* getGroupsSummaryEffect() {
  yield takeLatest(GroupsActionEnum.GET_GROUPS_SUMMARY, getGroupsSummary);
}
