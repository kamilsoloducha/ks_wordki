import { MainState } from "store/store";

export const selectIsSearching = (state: MainState) => state.groupsSearchReducer.isSearching;
export const selectFilter = (state: MainState) => state.groupsSearchReducer.groupName;
export const selectGroups = (state: MainState) => state.groupsSearchReducer.groups;
