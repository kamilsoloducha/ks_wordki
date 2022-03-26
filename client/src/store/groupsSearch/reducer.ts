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

    case act.GroupsSearchActionEnum.SET_GROUP:
      return act.setGroupReduce(state, action as act.SetGroup);
    case act.GroupsSearchActionEnum.RESET_SELECTION:
      return act.resetSelectionReduce(state);

    case act.GroupsSearchActionEnum.GET_CARDS:
      return act.getCardsReduce(state);
    case act.GroupsSearchActionEnum.GET_CARDS_SUCCESS:
      return act.getCardsSuccessReduce(state, action as act.GetCardsSuccess);

    case act.GroupsSearchActionEnum.SAVE_GROUP:
      return act.saveGroupReduce(state);
    case act.GroupsSearchActionEnum.SAVE_GROUP_SUCCESS:
      return act.saveGroupSuccessReduce(state);

    default:
      return state;
  }
}
