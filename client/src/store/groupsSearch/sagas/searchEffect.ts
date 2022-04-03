import * as actions from "../actions";
import * as api from "api";
import { all, call, put, select, takeLatest } from "@redux-saga/core/effects";
import { selectFilter } from "../selectors";
import { selectUserId } from "store/user/selectors";
import { GroupSummary } from "pages/groupsSearch/models/groupSummary";

function* search() {
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
  yield put(actions.searchSuccess(groups, count));
}

export function* searchEffect() {
  yield takeLatest(actions.GroupsSearchActionEnum.SEARCH, search);
}
