import { Action } from "@reduxjs/toolkit";
import { CardsOverview, CardSummary } from "pages/cardsSearch/models";
import { CardsSearchState } from "./state";

export enum CardsSearchActionEnum {
  GET_OVERVIEW = "[CARDS_SEARCH] GET_OVERVIEW",
  GET_OVERVIEW_SUCCESS = "[CARDS_SEARCH] GET_OVERVIEW_SUCCESS",

  SEARCH = "[CARDS_SEARCH] SEARCH",
  SEARCH_SUCCESS = "[CARDS_SEARCH] SEARCH_SUCCESS",

  FILTER_RESET = "[CARDS_SEARCH] FILTER_RESET",
  FILTER_SET_TERM = "[CARDS_SEARCH] FILTER_SET_TERM",
  FILTER_SET_PAGINATION = "[CARDS_SEARCH] FILTER_SET_PAGINATION",
  FILTER_SET_TICKED_ONLY = "[CARDS_SEARCH] FILTER_SET_TICKED_ONLY",
  FILTER_SET_LESSON_INCLUDED = "[CARDS_SEARCH] FILTER_SET_LESSON_INCLUDED",

  UPDATE_CARD = "[CARDS_SEARCH] UPDATE_CARD",
  UPDATE_CARD_SUCCESS = "[CARDS_SEARCH] UPDATE_CARD_SUCCESS",

  DELETE_CARD = "[CARDS_SEARCH] DELETE_CARD",
  DELETE_CARD_SUCCESS = "[CARDS_SEARCH] DELETE_CARD_SUCCESS",
}

export interface GetOverview extends Action {}
export function getOverview(): GetOverview {
  return {
    type: CardsSearchActionEnum.GET_OVERVIEW,
  };
}
export function getOverviewReduce(state: CardsSearchState): CardsSearchState {
  return {
    ...state,
  };
}

export interface GetOverviewSuccess extends Action {
  overview: CardsOverview;
}
export function getOverviewSuccess(overview: CardsOverview): GetOverviewSuccess {
  return {
    type: CardsSearchActionEnum.GET_OVERVIEW_SUCCESS,
    overview,
  };
}
export function getOverviewSuccessReduce(
  state: CardsSearchState,
  action: GetOverviewSuccess
): CardsSearchState {
  return {
    ...state,
    overview: action.overview,
  };
}

export interface Search extends Action {}
export function search(): Search {
  return {
    type: CardsSearchActionEnum.SEARCH,
  };
}
export function searchReduce(state: CardsSearchState): CardsSearchState {
  return {
    ...state,
    isSearching: true,
  };
}

export interface SeachSuccess extends Action {
  cards: CardSummary[];
}
export function searchSuccess(cards: CardSummary[]): SeachSuccess {
  return {
    type: CardsSearchActionEnum.SEARCH_SUCCESS,
    cards,
  };
}
export function searchSuccessReduce(
  state: CardsSearchState,
  action: SeachSuccess
): CardsSearchState {
  return {
    ...state,
    isSearching: false,
    cards: action.cards,
  };
}

export interface FilterReset extends Action {}
export function filterReset(): FilterReset {
  return { type: CardsSearchActionEnum.FILTER_RESET };
}
export function filterResetReduce(state: CardsSearchState): CardsSearchState {
  return {
    ...state,
    filter: {
      ...state.filter,
      searchingTerm: "",
      tickedOnly: null,
      lessonIncluded: null,
    },
  };
}

export interface FilterSetTerm extends Action {
  searchingTerm: string;
}
export function filterSetTerm(searchingTerm: string): FilterSetTerm {
  return {
    type: CardsSearchActionEnum.FILTER_SET_TERM,
    searchingTerm,
  };
}
export function filterSetTermReduce(
  state: CardsSearchState,
  action: FilterSetTerm
): CardsSearchState {
  return {
    ...state,
    filter: {
      ...state.filter,
      searchingTerm: action.searchingTerm,
    },
  };
}

export interface FilterSetTickedOnly extends Action {
  tickedOnly: boolean | null;
}
export function filterSetTickedOnly(tickedOnly: boolean | null): FilterSetTickedOnly {
  return {
    type: CardsSearchActionEnum.FILTER_SET_TICKED_ONLY,
    tickedOnly,
  };
}
export function filterSetTickedOnlyReduce(
  state: CardsSearchState,
  action: FilterSetTickedOnly
): CardsSearchState {
  return {
    ...state,
    filter: {
      ...state.filter,
      tickedOnly: action.tickedOnly,
    },
  };
}

export interface FilterSetLessonIncluded extends Action {
  lessonIncluded: boolean | null;
}
export function filterSetLessonIncluded(lessonIncluded: boolean | null): FilterSetLessonIncluded {
  return {
    type: CardsSearchActionEnum.FILTER_SET_LESSON_INCLUDED,
    lessonIncluded,
  };
}
export function filterSetLessonIncludedReduce(
  state: CardsSearchState,
  action: FilterSetLessonIncluded
): CardsSearchState {
  return {
    ...state,
    filter: {
      ...state.filter,
      lessonIncluded: action.lessonIncluded,
    },
  };
}

export interface FilterSetPagination extends Action {
  pageNumber: number;
  pageSize: number;
}
export function filterSetPagination(pageNumber: number, pageSize: number): FilterSetPagination {
  return {
    type: CardsSearchActionEnum.FILTER_SET_PAGINATION,
    pageNumber,
    pageSize,
  };
}
export function filterSetPaginationReducer(
  state: CardsSearchState,
  action: FilterSetPagination
): CardsSearchState {
  return {
    ...state,
    filter: {
      ...state.filter,
      pageNumber: action.pageNumber,
      pageSize: action.pageSize,
    },
  };
}

export interface UpdateCard extends Action {
  card: CardSummary;
}
export function udpateCard(card: CardSummary): UpdateCard {
  return {
    type: CardsSearchActionEnum.UPDATE_CARD,
    card,
  };
}
export function udpateCardReducer(state: CardsSearchState): CardsSearchState {
  return {
    ...state,
  };
}

export interface UpdateCardSuccess extends Action {
  card: CardSummary;
}
export function updateCardSuccess(card: CardSummary): UpdateCardSuccess {
  return {
    type: CardsSearchActionEnum.UPDATE_CARD_SUCCESS,
    card,
  };
}
export function UpdateCardSuccessReducer(
  state: CardsSearchState,
  action: UpdateCardSuccess
): CardsSearchState {
  return {
    ...state,
  };
}

export interface DeleteCard extends Action {
  cardId: number;
  groupId: number;
}
export function deleteCard(cardId: number, groupId: number): DeleteCard {
  return {
    type: CardsSearchActionEnum.DELETE_CARD,
    cardId,
    groupId,
  };
}
export function deleteCardReducer(state: CardsSearchState): CardsSearchState {
  return {
    ...state,
  };
}
