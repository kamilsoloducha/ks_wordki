import { MainState } from "store/store";

export const selectIsLoading = (state: MainState) =>
  state.groupsReducer.isLoading;

export const selectGroups = (state: MainState) => state.groupsReducer.groups;
export const selectSelectedItem = (state: MainState) =>
  state.groupsReducer.selectedItem;

export const selectSelectedItems = (state: MainState) =>
  state.groupsReducer.selectedItems;
