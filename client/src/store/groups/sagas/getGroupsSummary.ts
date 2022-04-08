import { call, put, select } from "@redux-saga/core/effects";
import { requestFailed } from "store/root/actions";
import { selectUserId } from "store/user/selectors";
import { getGroupsSummarySuccess, GroupsActionEnum } from "../actions";
import * as api from "api";
import { SagaIterator } from "redux-saga";
import { take } from "redux-saga/effects";

export function* getGroupsSummaryEffect(): SagaIterator {
  yield take(GroupsActionEnum.GET_GROUPS_SUMMARY);
  const userId: string = yield select(selectUserId);
  const { data, error }: { data: api.GroupsSummaryResponse; error: any } = yield call(() =>
    api.groups(userId)
  );
  yield put(data ? getGroupsSummarySuccess(data.groups) : requestFailed(error));
}
