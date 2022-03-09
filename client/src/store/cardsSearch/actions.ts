import { Action } from "@reduxjs/toolkit";
import { CardsOverview } from "pages/cards/models/cardsOverview";
import { CardSummary } from "pages/cards/models/cardSummary";
import { CardsSearchState } from "./state";

export enum CardsSearchActionEnum {
  GET_OVERVIEW = "[CARDS_SEARCH] GET_OVERVIEW",
  GET_OVERVIEW_SUCCESS = "[CARDS_SEARCH] GET_OVERVIEW_SUCCESS",

  SEARCH = "[CARDS_SEARCH] SEARCH",
  SEARCH_SUCCESS = "[CARDS_SEARCH] SEARCH_SUCCESS",

  FILTER_SET_TERM = "[CARDS_SEARCH] FILTER_SET_TERM",
  FILTER_SET_PAGINATION = "[CARDS_SEARCH] FILTER_SET_PAGINATION",
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
