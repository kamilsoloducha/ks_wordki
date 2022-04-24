import { CardsOverview, CardSummary } from "pages/cardsSearch/models";

export interface GetOverviewSuccess {
  overview: CardsOverview;
}

export interface SeachSuccess {
  cards: CardSummary[];
  cardsCount: number;
}

export interface FilterSetTerm {
  searchingTerm: string;
}

export interface FilterSetTickedOnly {
  tickedOnly: boolean | null;
}

export interface FilterSetLessonIncluded {
  lessonIncluded: boolean | null;
}

export interface FilterSetPagination {
  pageNumber: number;
  pageSize: number;
}

export interface UpdateCard {
  card: CardSummary;
}

export interface UpdateCardSuccess {
  card: CardSummary;
}

export interface DeleteCard {
  cardId: string;
  groupId: string;
}
