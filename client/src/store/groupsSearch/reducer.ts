import { Action, createSlice, PayloadAction } from "@reduxjs/toolkit";
import { GroupsSearchState, initialGroupsSearchState } from "./state";
import * as p from "./action-payloads";

export const groupSearchSlice = createSlice({
  name: "groupsSearch",
  initialState: initialGroupsSearchState,
  reducers: {
    search: (state: GroupsSearchState): void => {
      state.isSearching = true;
    },
    searchSuccess: (state: GroupsSearchState, action: PayloadAction<p.SearchSuccess>): void => {
      state.isSearching = false;
      state.groups = action.payload.groups;
      state.groupsCount = action.payload.groupsCount;
    },
    filterSetName: (state: GroupsSearchState, action: PayloadAction<p.FilterSetName>): void => {
      state.groupName = action.payload.name;
    },
    setGroup: (state: GroupsSearchState, action: PayloadAction<p.SetGroup>): void => {
      state.selectedGroup = action.payload.group;
    },
    resetSelection: (state: GroupsSearchState): void => {
      state.selectedGroup = null;
    },
    getCards: (state: GroupsSearchState): void => {
      state.isCardsLoading = true;
    },
    getCardsSuccess: (state: GroupsSearchState, action: PayloadAction<p.GetCardsSuccess>): void => {
      state.cards = action.payload.cards;
      state.isCardsLoading = false;
    },
    saveGroup: (state: GroupsSearchState): void => {
      state.isCardsLoading = true;
    },
    saveGroupSuccess: (state: GroupsSearchState): void => {
      state.isCardsLoading = false;
    },
  },
});

export default groupSearchSlice.reducer;

export const {
  filterSetName,
  getCards,
  getCardsSuccess,
  resetSelection,
  saveGroup,
  saveGroupSuccess,
  search,
  searchSuccess,
  setGroup,
} = groupSearchSlice.actions;
