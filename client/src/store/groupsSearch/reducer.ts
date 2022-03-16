import { Action } from "@reduxjs/toolkit";
import { GroupsSearchState, initialGroupsSearchState } from "./state";
import * as act from "./actions";

export function groupsSearchReducer(
  state = initialGroupsSearchState,
  action: Action
): GroupsSearchState {
  switch (action.type) {
    case act.GroupsSearchActionEnum.SEARCH:
      return act.searchReduce(state);
    case act.GroupsSearchActionEnum.SEARCH_SUCCESS:
      return act.searchSuccessReduce(state, action as act.SearchSuccess);

    case act.GroupsSearchActionEnum.FILTER_SET_NAME:
      return act.filterSetNameReduce(state, action as act.FilterSetName);
    default:
      return state;
  }
}
