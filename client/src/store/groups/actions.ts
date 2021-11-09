import { GroupSummary } from "pages/groups/models/groupSummary";
import GroupsState from "./state";

export enum GroupsActionEnum {
  GET_GROUPS_SUMMARY = "[GROUPS] GET_GROUPS_SUMMARY",
  GET_GROUPS_SUMMARY_SUCCESS = "[GROUPS] GET_GROUPS_SUMMARY_SUCCESS",

  SELECT_ITEM = "[GROUPS] SELECT_ITEM",
  RESET_SELECTED_ITEM = "[GROUPS] RESET_SELECTED_ITEM",
}

export interface GroupsAction {
  type: GroupsActionEnum;
  reduce: (state: GroupsState) => GroupsState;
}

export interface GetGroupsSummary extends GroupsAction {}

export function getGroupsSummary(): GetGroupsSummary {
  return {
    type: GroupsActionEnum.GET_GROUPS_SUMMARY,
    reduce: (state: GroupsState): GroupsState => {
      return { ...state, isLoading: true };
    },
  };
}

export interface GetGroupsSummarySuccess extends GroupsAction {
  groups: GroupSummary[];
}

export function getGroupsSummarySuccess(
  groups: GroupSummary[]
): GetGroupsSummarySuccess {
  return {
    groups,
    type: GroupsActionEnum.GET_GROUPS_SUMMARY_SUCCESS,
    reduce: (state: GroupsState): GroupsState => {
      return { ...state, isLoading: false, groups };
    },
  };
}

export interface SelectItem extends GroupsAction {}

export function selectItem(selectedItem: GroupSummary): SelectItem {
  return {
    type: GroupsActionEnum.SELECT_ITEM,
    reduce: (state: GroupsState): GroupsState => {
      return { ...state, selectedItem };
    },
  };
}

export interface ResetSelectedItem extends GroupsAction {}

export function resetSelectedItem(): ResetSelectedItem {
  return {
    type: GroupsActionEnum.RESET_SELECTED_ITEM,
    reduce: (state: GroupsState): GroupsState => {
      return { ...state, selectedItem: null };
    },
  };
}
