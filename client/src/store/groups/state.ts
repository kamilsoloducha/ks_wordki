import { GroupSummary } from "pages/groups/models/groupSummary";

export default interface GroupsState {
  isLoading: boolean;
  groups: GroupSummary[];
  selectedItem: GroupSummary | null;
  selectedItems: number[];
}

export const initialState: GroupsState = {
  isLoading: true,
  groups: [],
  selectedItem: null,
  selectedItems: [],
};
