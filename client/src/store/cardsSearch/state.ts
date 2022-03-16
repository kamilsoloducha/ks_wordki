import { CardsOverview, CardSummary, Filter } from "pages/cardsSearch/models";

export interface CardsSearchState {
  isSearching: boolean;
  cards: CardSummary[];
  cardsCount: number;
  overview: CardsOverview;
  filter: Filter;
}

export const initialCardsSearchState: CardsSearchState = {
  isSearching: false,
  cards: [],
  cardsCount: 0,
  overview: { all: 0, drawers: [0, 0, 0, 0, 0], lessonIncluded: 0, ticked: 0 },
  filter: {
    searchingTerm: "",
    tickedOnly: null,
    lessonIncluded: null,
    pageNumber: 1,
    pageSize: 50,
  },
};
