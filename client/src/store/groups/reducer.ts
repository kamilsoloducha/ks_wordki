import { createSlice, PayloadAction } from '@reduxjs/toolkit'
import GroupsState, { initialState } from './state'
import * as p from './action-payloads'

export const groupsSlice = createSlice({
  name: 'groups',
  initialState,
  reducers: {
    getGroupsSummary: (state: GroupsState): void => {
      state.isLoading = true
    },
    getGroupsSummarySuccess: (
      state: GroupsState,
      action: PayloadAction<p.GetGroupsSummarySuccess>
    ): void => {
      state.isLoading = false
      state.groups = action.payload.groups
    },
    addGroup: (state: GroupsState, _: PayloadAction<p.AddGroup>): void => {},
    updateGroup: (_: GroupsState, __: PayloadAction<p.UpdateGroup>): void => {}
  }
})

export default groupsSlice.reducer

export const { addGroup, getGroupsSummary, getGroupsSummarySuccess, updateGroup } =
  groupsSlice.actions
