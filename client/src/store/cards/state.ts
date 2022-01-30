import { CardSummary } from "pages/cards/models/groupDetailsSummary";

export default interface CardsState {
  isLoading: boolean;
  id: number;
  name: string;
  language1: number;
  language2: number;
  cards: CardSummary[];
  selectedItem: CardSummary | null;
}

export const initialState: CardsState = {
  isLoading: false,
  id: 0,
  name: "",
  language1: 0,
  language2: 0,
  cards: [],
  selectedItem: null,
};
