import { GroupSummary } from 'common/models/groupSummary'

export default interface GroupsState {
  isLoading: boolean
  groups: GroupSummary[]
  selectedItems: string[]
  searchingGroups: GroupSummary[]
}

export const initialState: GroupsState = {
  isLoading: true,
  groups: [],
  selectedItems: [],
  searchingGroups: []
}
