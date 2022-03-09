import { CardsOverview } from "pages/cards/models/cardsOverview";
import { Filter } from "pages/cards/models/filter";
import { CardSummary } from "pages/cards/models/groupDetailsSummary";

export interface CardsSearchState {
  isSearching: boolean;
  cards: CardSummary[];
  overview: CardsOverview;
  filter: Filter;
}

export const initialCardsSearchState: CardsSearchState = {
  isSearching: false,
  cards: [],
  overview: { all: 0, drawers: [0, 0, 0, 0, 0], lessonIncluded: 0, ticked: 0 },
  filter: { searchingTerm: "", pageNumber: 1, pageSize: 50 },
};
