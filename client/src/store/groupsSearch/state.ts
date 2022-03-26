import { CardSummary } from "pages/groupsSearch/models/cardSummary";
import { GroupSummary } from "pages/groupsSearch/models/groupSummary";

export interface GroupsSearchState {
  isSearching: boolean;

  groups: GroupSummary[];
  groupsCount: number;

  selectedGroup: GroupSummary | null;
  isCardsLoading: boolean;
  cards: CardSummary[];

  groupName: string;
}

export const initialGroupsSearchState: GroupsSearchState = {
  isSearching: false,

  groups: [],
  groupsCount: 0,

  selectedGroup: null,
  isCardsLoading: false,
  cards: [],

  groupName: "",
};
