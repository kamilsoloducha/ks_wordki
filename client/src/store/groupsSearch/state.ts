import { GroupSummary } from "pages/groupsSearch/models/groupSummary";

export interface GroupsSearchState {
  isSearching: boolean;
  groups: GroupSummary[];
  groupsCount: number;

  groupName: string;
}

export const initialGroupsSearchState: GroupsSearchState = {
  isSearching: false,

  groups: [],
  groupsCount: 0,

  groupName: "",
};
