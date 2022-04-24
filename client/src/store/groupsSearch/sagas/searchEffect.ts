import * as api from "api";
import { all, call, put, select, take } from "@redux-saga/core/effects";
import { selectFilter } from "../selectors";
import { selectUserId } from "store/user/selectors";
import { GroupSummary } from "pages/groupsSearch/models/groupSummary";
import { SagaIterator } from "redux-saga";
import { searchSuccess } from "../reducer";

export function* searchEffect(): SagaIterator {
  yield take("groupsSearch/search");
  const userId: string = yield select(selectUserId);
  const filter: string = yield select(selectFilter);

  const searchRequest: api.SearchGroupsQuery = {
    ownerId: userId,
    name: filter,
    pageNumber: 0,
    pageSize: 100,
  };

  const [groups, count]: [GroupSummary[], number] = yield all([
    call(api.searchGroups, searchRequest),
    call(api.searchGroupCount, searchRequest),
  ]);
  yield put(searchSuccess({ groups, groupsCount: count }));
}
