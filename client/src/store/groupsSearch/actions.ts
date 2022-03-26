import { Action } from "@reduxjs/toolkit";
import { CardSummary } from "pages/groupsSearch/models/cardSummary";
import { GroupSummary } from "pages/groupsSearch/models/groupSummary";
import { GroupsSearchState } from "./state";

export enum GroupsSearchActionEnum {
  SEARCH = "[GROUPS_SERACH] SEARCH",
  SEARCH_SUCCESS = "[GROUPS_SERACH] SEARCH_SUCCESS",

  FILTER_SET_NAME = "[GROUPS_SERACH] FILTER_SET_NAME",

  SET_GROUP = "[GROUPS_SERACH] SET_GROUP",
  RESET_SELECTION = "[GROUPS_SERACH] RESET_SELECTION",

  GET_CARDS = "[GROUPS_SERACH] GET_CARDS",
  GET_CARDS_SUCCESS = "[GROUPS_SERACH] GET_CARDS_SUCCESS",

  SAVE_GROUP = "[GROUPS_SERACH] SAVE_GROUP",
  SAVE_GROUP_SUCCESS = "[GROUPS_SERACH] SAVE_GROUP_SUCCESS",
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

export interface SetGroup extends Action {
  group: GroupSummary;
}
export function setGroup(group: GroupSummary): SetGroup {
  return {
    type: GroupsSearchActionEnum.SET_GROUP,
    group,
  };
}
export function setGroupReduce(state: GroupsSearchState, action: SetGroup): GroupsSearchState {
  return {
    ...state,
    selectedGroup: action.group,
  };
}

export interface ResetSelection extends Action {}
export function resetSelection(): ResetSelection {
  return {
    type: GroupsSearchActionEnum.RESET_SELECTION,
  };
}
export function resetSelectionReduce(state: GroupsSearchState): GroupsSearchState {
  return {
    ...state,
    selectedGroup: null,
  };
}

export interface GetCards extends Action {}
export function getCards(): GetCards {
  return {
    type: GroupsSearchActionEnum.GET_CARDS,
  };
}
export function getCardsReduce(state: GroupsSearchState): GroupsSearchState {
  return {
    ...state,
    isCardsLoading: true,
  };
}

export interface GetCardsSuccess extends Action {
  cards: CardSummary[];
}
export function getCardsSuccess(cards: CardSummary[]): GetCardsSuccess {
  return {
    type: GroupsSearchActionEnum.GET_CARDS_SUCCESS,
    cards,
  };
}
export function getCardsSuccessReduce(
  state: GroupsSearchState,
  action: GetCardsSuccess
): GroupsSearchState {
  return {
    ...state,
    isCardsLoading: false,
    cards: action.cards,
  };
}

export interface SaveGroup extends Action {}
export function saveGroup(): SaveGroup {
  return {
    type: GroupsSearchActionEnum.SAVE_GROUP,
  };
}
export function saveGroupReduce(state: GroupsSearchState): GroupsSearchState {
  return {
    ...state,
    isCardsLoading: true,
  };
}

export interface SaveGroupSuccess extends Action {}
export function saveGroupSuccess(): SaveGroupSuccess {
  return {
    type: GroupsSearchActionEnum.SAVE_GROUP_SUCCESS,
  };
}
export function saveGroupSuccessReduce(state: GroupsSearchState): GroupsSearchState {
  return {
    ...state,
    isCardsLoading: false,
  };
}
