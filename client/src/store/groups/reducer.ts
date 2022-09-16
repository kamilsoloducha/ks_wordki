import {createSlice, PayloadAction } from "@reduxjs/toolkit";
import GroupsState, { initialState } from "./state";
import * as p from "./action-payloads";
import { GroupSummary } from "pages/groups/models/groupSummary";

export const groupsSlice = createSlice({
  name: "groups",
  initialState: initialState,
  reducers: {
    getGroupsSummary: (state: GroupsState): void => {
      state.isLoading = true;
      state.selectedItem = null;
    },
    getGroupsSummarySuccess: (
      state: GroupsState,
      action: PayloadAction<p.GetGroupsSummarySuccess>
    ): void => {
      state.isLoading = false;
      state.groups = action.payload.groups;
    },
    selectItemById: (state: GroupsState, action: PayloadAction<p.SelectById>): void => {
      const selectedItem = state.groups.find(
        (x) => x.id === action.payload.groupId
      ) as GroupSummary;
      state.selectedItem = selectedItem;
    },
    selectItem: (state: GroupsState, action: PayloadAction<p.Select>): void => {
      state.selectedItem = action.payload.group;
    },
    resetSelectedItem: (state: GroupsState): void => {
      state.selectedItem = null;
    },
    addGroup: (state: GroupsState, _: PayloadAction<p.AddGroup>): void => {},
    addGroupSuccess: (state: GroupsState): void => {
      state.selectedItem = null;
    },
    updateGroup: (state: GroupsState, _: PayloadAction<p.UpdateGroup>): void => {},
    updateGroupSuccess: (state: GroupsState): void => {
      state.selectedItem = null;
    },
  },
});

export default groupsSlice.reducer;

export const {
  addGroup,
  addGroupSuccess,
  getGroupsSummary,
  getGroupsSummarySuccess,
  resetSelectedItem,
  selectItem,
  selectItemById,
  updateGroup,
  updateGroupSuccess,
} = groupsSlice.actions;
