import { Action } from "@reduxjs/toolkit";
import { GroupSummary } from "pages/groupsSearch/models/groupSummary";
import { GroupsSearchState } from "./state";

export enum GroupsSearchActionEnum {
  SEARCH = "[GROUPS_SERACH] SEARCH",
  SEARCH_SUCCESS = "[GROUPS_SERACH] SEARCH_SUCCESS",

  FILTER_SET_NAME = "[GROUPS_SERACH] FILTER_SET_NAME",
}

export interface Search extends Action {}
export function search(): Search {
  return {
    type: GroupsSearchActionEnum.SEARCH,
  };
}
export function searchReduce(state: GroupsSearchState): GroupsSearchState {
  return {
    ...state,
    isSearching: true,
  };
}

export interface SearchSuccess extends Action {
  groups: GroupSummary[];
  groupsCount: number;
}
export function searchSuccess(groups: GroupSummary[], groupsCount: number): SearchSuccess {
  return {
    type: GroupsSearchActionEnum.SEARCH_SUCCESS,
    groups,
    groupsCount,
  };
}
export function searchSuccessReduce(
  state: GroupsSearchState,
  action: SearchSuccess
): GroupsSearchState {
  return {
    ...state,
    groups: action.groups,
    groupsCount: action.groupsCount,
    isSearching: false,
  };
}

export interface FilterSetName extends Action {
  name: string;
}
export function filterSetName(name: string): FilterSetName {
  return {
    type: GroupsSearchActionEnum.FILTER_SET_NAME,
    name,
  };
}
export function filterSetNameReduce(
  state: GroupsSearchState,
  action: FilterSetName
): GroupsSearchState {
  return {
    ...state,
    groupName: action.name,
  };
}
