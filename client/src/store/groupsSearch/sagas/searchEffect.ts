import * as actions from "../actions";
import * as api from "pages/groupsSearch/services/groupsSearchApi";
import { all, call, put, select, takeLatest } from "@redux-saga/core/effects";
import { selectFilter } from "../selectors";
import { selectUserId } from "store/user/selectors";
import { SearchGroupsRequest } from "pages/groupsSearch/models/requests/searchGroupsRequest";
import { GroupSummary } from "pages/groupsSearch/models/groupSummary";

function* search() {
  const userId: string = yield select(selectUserId);
  const filter: string = yield select(selectFilter);

  const searchRequest: SearchGroupsRequest = {
    ownerId: userId,
    name: filter,
    pageNumber: 0,
    pageSize: 100,
  };

  const [groups, count]: [GroupSummary[], number] = yield all([
    call(api.searchCards, searchRequest),
    call(api.searchCardsCount, searchRequest),
  ]);

  yield put(actions.searchSuccess(groups, count));
}

export function* searchEffect() {
  yield takeLatest(actions.GroupsSearchActionEnum.SEARCH, search);
}
