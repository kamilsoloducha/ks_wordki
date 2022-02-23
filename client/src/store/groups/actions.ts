import GroupDetails from "common/components/dialogs/groupDialog/groupDetails";
import { GroupSummary } from "pages/groups/models/groupSummary";
import GroupsState from "./state";

export enum GroupsActionEnum {
  GET_GROUPS_SUMMARY = "[GROUPS] GET_GROUPS_SUMMARY",
  GET_GROUPS_SUMMARY_SUCCESS = "[GROUPS] GET_GROUPS_SUMMARY_SUCCESS",

  SELECTION_CHANGED = "[GROUPS] SELECTION_CHANGED",
  SELECT_ITEM = "[GROUPS] SELECT_ITEM",
  RESET_SELECTED_ITEM = "[GROUPS] RESET_SELECTED_ITEM",

  ADD_GROUP = "[GROUPS] ADD_GROUP",
  ADD_GROUP_SUCCESS = "[GROUPS] ADD_GROUP_SUCCESS",

  UPDATE_GROUP = "[GROUPS] UPDATE_GROUP",
  UPDATE_GROUP_SUCCESS = "[GROUPS] UPDATE_GROUP_SUCCESS",

  CONNECT_GROUPS = "[GROUPS] CONNECT_GROUPS",
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
      return { ...state, isLoading: true, selectedItem: null };
    },
  };
}

export interface GetGroupsSummarySuccess extends GroupsAction {
  groups: GroupSummary[];
}

export function getGroupsSummarySuccess(groups: GroupSummary[]): GetGroupsSummarySuccess {
  return {
    groups,
    type: GroupsActionEnum.GET_GROUPS_SUMMARY_SUCCESS,
    reduce: (state: GroupsState): GroupsState => {
      return { ...state, isLoading: false, groups, selectedItems: [] };
    },
  };
}

export interface SelectionChanged extends GroupsAction {}
export function selectionChanged(id: number, isSelected: boolean): SelectionChanged {
  return {
    type: GroupsActionEnum.SELECTION_CHANGED,
    reduce: (state: GroupsState): GroupsState => {
      const selectedItems = isSelected
        ? [...state.selectedItems, id]
        : state.selectedItems.filter((x) => x !== id);
      return { ...state, selectedItems };
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

export function selectItemById(groupId: number): SelectItem {
  return {
    type: GroupsActionEnum.SELECT_ITEM,
    reduce: (state: GroupsState): GroupsState => {
      const selectedItem = state.groups.find((x) => x.id === groupId) as GroupSummary;
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

export interface AddGroup extends GroupsAction {
  group: GroupDetails;
}
export function addGroup(group: GroupDetails): AddGroup {
  return {
    group,
    type: GroupsActionEnum.ADD_GROUP,
    reduce: (state: GroupsState): GroupsState => {
      return { ...state };
    },
  };
}

export interface AddGroupSuccess extends GroupsAction {}
export function addGroupSuccess(): AddGroupSuccess {
  return {
    type: GroupsActionEnum.ADD_GROUP_SUCCESS,
    reduce: (state: GroupsState): GroupsState => {
      return { ...state, selectedItem: null };
    },
  };
}

export interface UpdateGroup extends GroupsAction {
  group: GroupDetails;
}
export function updateGroup(group: GroupDetails): UpdateGroup {
  return {
    group,
    type: GroupsActionEnum.UPDATE_GROUP,
    reduce: (state: GroupsState): GroupsState => {
      return { ...state };
    },
  };
}

export interface UpdateGroupSuccess extends GroupsAction {}
export function updateGroupSuccess(): UpdateGroupSuccess {
  return {
    type: GroupsActionEnum.UPDATE_GROUP_SUCCESS,
    reduce: (state: GroupsState): GroupsState => {
      return { ...state, selectedItem: null };
    },
  };
}

export interface ConnectGroups extends GroupsAction {}
export function connectGroups(): ConnectGroups {
  return {
    type: GroupsActionEnum.CONNECT_GROUPS,
    reduce: (state: GroupsState): GroupsState => {
      return { ...state };
    },
  };
}
