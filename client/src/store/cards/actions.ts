import { CardSummary } from "pages/cards/models/groupDetailsSummary";
import CardsState from "./state";

export enum CardsActionEnum {
  GET_CARDS = "[CARDS] GET_CARDS",
  GET_CARDS_SUCCESS = "[CARDS] GET_CARDS_SUCCESS",

  SELECT_CARD = "[CARDS] SELECT_CARD",
  RESET_SELECTED_CARD = "[CARDS] RESET_SELECTED_CARD",

  ADD_CARD = "[CARDS] ADD_CARD",
  ADD_CARD_SUCCESS = "[CARDS] ADD_CARD_SUCCESS",

  UPDATE_CARD = "[CARDS] UPDATE_CARD",
  UPDATE_CARD_SUCCESS = "[CARDS] UPDATE_CARD_SUCCESS",

  DELETE_CARD = "[CARDS] DELETE_CARD",
  DELETE_CARD_SUCCESS = "[CARDS] DELETE_CARD_SUCCESS",

  APPEND_CARD = "[CARDS] DELETE_CARD",
}

export interface CardsAction {
  type: CardsActionEnum;
  reduce: (state: CardsState) => CardsState;
}

export interface GetCards extends CardsAction {
  groupId: number;
}

export function getCards(groupId: number): GetCards {
  return {
    groupId,
    type: CardsActionEnum.GET_CARDS,
    reduce: (state: CardsState): CardsState => {
      return { ...state, isLoading: true };
    },
  };
}

export interface GetCardsSuccess extends CardsAction {
  cards: CardSummary[];
}

export function getCardsSuccess(
  id: number,
  name: string,
  language1: number,
  language2: number,
  cards: CardSummary[]
): GetCardsSuccess {
  return {
    cards,
    type: CardsActionEnum.GET_CARDS_SUCCESS,
    reduce: (state: CardsState): CardsState => {
      return {
        ...state,
        id,
        name,
        language1,
        language2,
        cards,
        isLoading: false,
        selectedItem: null,
      };
    },
  };
}

export interface SelectCard extends CardsAction {}

export function selectCard(item: CardSummary): SelectCard {
  return {
    type: CardsActionEnum.SELECT_CARD,
    reduce: (state: CardsState): CardsState => {
      return {
        ...state,
        selectedItem: item,
      };
    },
  };
}

export interface UpdateCard extends CardsAction {
  card: CardSummary;
}

export function updateCard(card: CardSummary): UpdateCard {
  return {
    card,
    type: CardsActionEnum.UPDATE_CARD,
    reduce: (state: CardsState): CardsState => {
      return { ...state };
    },
  };
}

export interface UpdateCardSuccess extends CardsAction {}

export function updateCardSuccess(card: CardSummary): UpdateCardSuccess {
  return {
    type: CardsActionEnum.UPDATE_CARD_SUCCESS,
    reduce: (state: CardsState): CardsState => {
      const cards = state.cards.map((item) => {
        return item.id === card.id ? card : item;
      });
      return { ...state, cards, selectedItem: null };
    },
  };
}

export interface AddCard extends CardsAction {
  card: CardSummary;
}

export function addCard(card: CardSummary): AddCard {
  return {
    card,
    type: CardsActionEnum.ADD_CARD,
    reduce: (state: CardsState): CardsState => {
      return { ...state };
    },
  };
}

export interface AddCardSuccess extends CardsAction {}

export function addCardSuccess(): AddCardSuccess {
  return {
    type: CardsActionEnum.ADD_CARD_SUCCESS,
    reduce: (state: CardsState): CardsState => {
      return { ...state, selectedItem: null };
    },
  };
}

export interface DeleteCard extends CardsAction {}

export function deleteCard(): DeleteCard {
  return {
    type: CardsActionEnum.DELETE_CARD,
    reduce: (state: CardsState): CardsState => {
      return { ...state };
    },
  };
}

export interface DeleteCardSuccess extends CardsAction {}

export function deleteCardSuccess(cardId: number): DeleteCardSuccess {
  return {
    type: CardsActionEnum.DELETE_CARD_SUCCESS,
    reduce: (state: CardsState): CardsState => {
      const cards = state.cards.filter((x) => x.id !== cardId);
      return { ...state, cards, selectedItem: null };
    },
  };
}

export interface ResetSelectedItem extends CardsAction {}
export function resetSelectedCard(): ResetSelectedItem {
  return {
    type: CardsActionEnum.RESET_SELECTED_CARD,
    reduce: (state: CardsState): CardsState => {
      return { ...state, selectedItem: null };
    },
  };
}

export interface AppendCard extends CardsAction {
  groupId: string;
  count: number;
  languages: number;
}
export function appendCard(
  groupId: string,
  count: number,
  languages: number
): AppendCard {
  return {
    type: CardsActionEnum.APPEND_CARD,
    reduce: (state: CardsState): CardsState => {
      return { ...state };
    },
    groupId,
    count,
    languages,
  };
}
