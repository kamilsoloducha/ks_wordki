import { CardSummary } from "pages/cards/models";
import { Action } from "redux";
import CardsState, { initialState } from "./state";

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

  SET_FILTER_DRAWER = "[CARDS] SET_FILTER_DRAWER",
  SET_FILTER_LEARNING = "[CARDS] SET_FILTER_LEARNING",
  SET_FILTER_TEXT = "[CARDS] SET_FILTER_TEXT",
  SET_FILTER_IS_TICKED = "[CARDS] SET_FILTER_IS_TICKED",
  RESET_FILTERS = "[CARDS] RESET_FILTERS",
  SET_FILTERED_CARDS = "[CARDS] SET_FILTERED_CARDS",
  APPLY_FILTERS = "[CARDS] APPLY_FILTERS",
}

export interface GetCards extends Action {
  groupId: string;
}
export function getCards(groupId: string): GetCards {
  return {
    type: CardsActionEnum.GET_CARDS,
    groupId,
  };
}
export function reduceGetCards(state: CardsState): CardsState {
  return { ...state, isLoading: true };
}

export interface GetCardsSuccess extends Action {
  id: string;
  name: string;
  language1: number;
  language2: number;
  cards: CardSummary[];
}
export function getCardsSuccess(
  id: string,
  name: string,
  language1: number,
  language2: number,
  cards: CardSummary[]
): GetCardsSuccess {
  return {
    id,
    name,
    language1,
    language2,
    cards,
    type: CardsActionEnum.GET_CARDS_SUCCESS,
  };
}
export function reduceGetCardsSuccess(state: CardsState, action: GetCardsSuccess): CardsState {
  return {
    ...state,
    id: action.id,
    name: action.name,
    language1: action.language1,
    language2: action.language2,
    cards: action.cards,
    isLoading: false,
    selectedItem: null,
  };
}

export interface SelectCard extends Action {
  item: CardSummary;
}
export function selectCard(item: CardSummary): SelectCard {
  return {
    item,
    type: CardsActionEnum.SELECT_CARD,
  };
}
export function reduceSelectCard(state: CardsState, action: SelectCard): CardsState {
  return {
    ...state,
    selectedItem: action.item,
  };
}

export interface UpdateCard extends Action {
  card: CardSummary;
}
export function updateCard(card: CardSummary): UpdateCard {
  return {
    card,
    type: CardsActionEnum.UPDATE_CARD,
  };
}
export function reduceUpdateCard(state: CardsState, action: UpdateCard): CardsState {
  return { ...state };
}

export interface UpdateCardSuccess extends Action {
  card: CardSummary;
}

export function updateCardSuccess(card: CardSummary): UpdateCardSuccess {
  return {
    card,
    type: CardsActionEnum.UPDATE_CARD_SUCCESS,
  };
}
export function reduceUpdateCardSuccess(state: CardsState, action: UpdateCardSuccess): CardsState {
  const card = action.card;
  const cards = state.cards.map((item) => {
    return item.id === card.id ? { ...item, card } : item;
  });
  return { ...state, cards, selectedItem: null };
}

export interface AddCard extends Action {
  card: CardSummary;
}
export function addCard(card: CardSummary): AddCard {
  return {
    card,
    type: CardsActionEnum.ADD_CARD,
  };
}
export function reduceAddCard(state: CardsState): CardsState {
  return { ...state };
}

export interface AddCardSuccess extends Action {}
export function addCardSuccess(): AddCardSuccess {
  return {
    type: CardsActionEnum.ADD_CARD_SUCCESS,
  };
}
export function reduceAddCardSuccess(state: CardsState): CardsState {
  return { ...state, selectedItem: null };
}

export interface DeleteCard extends Action {}
export function deleteCard(): DeleteCard {
  return {
    type: CardsActionEnum.DELETE_CARD,
  };
}
export function reduceDeleteCard(state: CardsState): CardsState {
  return { ...state };
}

export interface DeleteCardSuccess extends Action {
  cardId: string;
}
export function deleteCardSuccess(cardId: string): DeleteCardSuccess {
  return {
    cardId,
    type: CardsActionEnum.DELETE_CARD_SUCCESS,
  };
}
export function reduceDeleteCardSuccess(state: CardsState, action: DeleteCardSuccess): CardsState {
  const cards = state.cards.filter((x) => x.id !== action.cardId);
  return { ...state, cards, selectedItem: null };
}

export interface ResetSelectedCard extends Action {}
export function resetSelectedCard(): ResetSelectedCard {
  return {
    type: CardsActionEnum.RESET_SELECTED_CARD,
  };
}
export function reduceResetSelectedCard(state: CardsState): CardsState {
  return { ...state, selectedItem: null };
}

export interface SetFilterDrawer extends Action {
  drawer: number;
}
export function setFilterDrawer(drawer: number): SetFilterDrawer {
  return {
    type: CardsActionEnum.SET_FILTER_DRAWER,
    drawer,
  };
}
export function reduceSetFilterDrawer(state: CardsState, action: SetFilterDrawer): CardsState {
  return {
    ...state,
    filter: {
      ...state.filter,
      drawer: action.drawer,
    },
  };
}

export interface SetFilterLearning extends Action {
  isLearning: boolean;
}
export function setFilterLearning(isLearning: boolean): SetFilterLearning {
  return {
    type: CardsActionEnum.SET_FILTER_LEARNING,
    isLearning,
  };
}
export function reduceSetFilterLearning(state: CardsState, action: SetFilterLearning): CardsState {
  return {
    ...state,
    filter: {
      ...state.filter,
      isLearning: action.isLearning,
    },
  };
}

export interface SetFilterText extends Action {
  text: string;
}
export function setFilterText(text: string): SetFilterText {
  return {
    type: CardsActionEnum.SET_FILTER_TEXT,
    text,
  };
}
export function reduceSetFilterText(state: CardsState, action: SetFilterText): CardsState {
  return {
    ...state,
    filter: {
      ...state.filter,
      text: action.text,
    },
  };
}

export interface SetFilterIsTicked extends Action {
  isTicked: boolean;
}
export function setFilterIsTicked(isTicked: boolean): SetFilterIsTicked {
  return {
    type: CardsActionEnum.SET_FILTER_IS_TICKED,
    isTicked,
  };
}
export function reduceSetFilterIsTicked(state: CardsState, action: SetFilterIsTicked): CardsState {
  return {
    ...state,
    filter: {
      ...state.filter,
      isTicked: action.isTicked,
    },
  };
}

export function resetFilter(): Action {
  return {
    type: CardsActionEnum.RESET_FILTERS,
  };
}
export function reduceResetFilter(state: CardsState): CardsState {
  return {
    ...state,
    filter: initialState.filter,
  };
}

export interface SetFilteredCards extends Action {
  cards: CardSummary[];
}
export function setFilteredCards(cards: CardSummary[]): SetFilteredCards {
  return {
    type: CardsActionEnum.SET_FILTERED_CARDS,
    cards,
  };
}
export function reduceSetFilteredCards(state: CardsState, action: SetFilteredCards): CardsState {
  return {
    ...state,
    filteredCards: action.cards,
  };
}

export interface ApplyFilters extends Action {}
export function applyFilters(): ApplyFilters {
  return {
    type: CardsActionEnum.APPLY_FILTERS,
  };
}
export function reduceApplyFilters(state: CardsState): CardsState {
  return {
    ...state,
  };
}
