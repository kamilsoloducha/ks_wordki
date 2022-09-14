import { call, put, select } from "@redux-saga/core/effects";
import { requestFailed } from "store/root/actions";
import { selectUserId } from "store/user/selectors";
import * as api from "api";
import { SagaIterator } from "redux-saga";
import { take } from "redux-saga/effects";
import { getGroupsSummarySuccess } from "../reducer";

export function* getGroupsSummaryEffect(): SagaIterator {
  while(true){
    yield take("groups/getGroupsSummary");
    const userId: string = yield select(selectUserId);
    const { data, error }: { data: api.GroupsSummaryResponse; error: any } = yield call(
      api.groups,
      userId
    );
    yield put(data ? getGroupsSummarySuccess({ groups: data.groups }) : requestFailed(error));
  }
}
