import { CardSummary } from "pages/cards/models";

export default interface CardsState {
  isLoading: boolean;
  id: string;
  name: string;
  language1: number;
  language2: number;
  cards: CardSummary[];
  filteredCards: CardSummary[];
  selectedItem: CardSummary | null;
  filter: FilterModel;
}

export const initialState: CardsState = {
  isLoading: false,
  id: "",
  name: "",
  language1: 0,
  language2: 0,
  cards: [],
  filteredCards: [],
  selectedItem: null,
  filter: {
    drawer: null,
    isLearning: null,
    text: "",
    isTicked: false,
  },
};

export interface FilterModel {
  drawer: number | null;
  isLearning: boolean | null;
  text: string;
  isTicked: boolean;
}
