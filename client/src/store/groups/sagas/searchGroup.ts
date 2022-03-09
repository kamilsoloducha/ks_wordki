import { call, put, takeLatest } from "@redux-saga/core/effects";
import { requestFailed } from "store/root/actions";
import { GroupsActionEnum, SearchGroup, searchGroupSuccess } from "../actions";
import * as api from "pages/groups/services/groupsApi";
import { SearchGroupRequest } from "pages/groups/models/searchGroupRequest";

function* searchGroup({ searchingText }: SearchGroup) {
  const request: SearchGroupRequest = {
    SearchingTerm: searchingText,
    pageNumber: 0,
    pageSize: 1000,
  };
  const apiResponse: any[] = yield call(api.searchGroup, request);
  yield put(
    apiResponse.length > 0 ? searchGroupSuccess(apiResponse) : requestFailed("error" as any)
  );
}

export function* searchGroupEffect() {
  yield takeLatest(GroupsActionEnum.SEARCH_GROUP, searchGroup);
}
