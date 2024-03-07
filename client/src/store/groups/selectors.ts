import { MainState } from 'store/store'

export const selectIsLoading = (state: MainState) => state.groupsReducer.isLoading

export const selectGroups = (state: MainState) => state.groupsReducer.groups

export const selectSelectedItems = (state: MainState) => state.groupsReducer.selectedItems

export const selectSearchingItems = (state: MainState) => state.groupsReducer.searchingGroups
