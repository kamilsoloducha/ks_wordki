import { CardSummary } from "pages/groupsSearch/models/cardSummary";
import { GroupSummary } from "pages/groupsSearch/models/groupSummary";

export interface SearchSuccess {
  groups: GroupSummary[];
  groupsCount: number;
}

export interface FilterSetName {
  name: string;
}

export interface SetGroup {
  group: GroupSummary;
}

export interface GetCardsSuccess {
  cards: CardSummary[];
}
