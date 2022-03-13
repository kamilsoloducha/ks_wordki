import { Action } from "@reduxjs/toolkit";
import * as a from "./actions";
import { CardsSearchState, initialCardsSearchState } from "./state";

export default function cardsSeachReducer(
  state = initialCardsSearchState,
  action: Action
): CardsSearchState {
  switch (action.type) {
    case a.CardsSearchActionEnum.SEARCH:
      return a.searchReduce(state);
    case a.CardsSearchActionEnum.SEARCH_SUCCESS:
      return a.searchSuccessReduce(state, action as a.SeachSuccess);
    case a.CardsSearchActionEnum.GET_OVERVIEW:
      return a.getOverviewReduce(state);
    case a.CardsSearchActionEnum.GET_OVERVIEW_SUCCESS:
      return a.getOverviewSuccessReduce(state, action as a.GetOverviewSuccess);
    case a.CardsSearchActionEnum.FILTER_RESET:
      return a.filterResetReduce(state);
    case a.CardsSearchActionEnum.FILTER_SET_TERM:
      return a.filterSetTermReduce(state, action as a.FilterSetTerm);
    case a.CardsSearchActionEnum.FILTER_SET_TICKED_ONLY:
      return a.filterSetTickedOnlyReduce(state, action as a.FilterSetTickedOnly);
    case a.CardsSearchActionEnum.FILTER_SET_LESSON_INCLUDED:
      return a.filterSetLessonIncludedReduce(state, action as a.FilterSetLessonIncluded);
    case a.CardsSearchActionEnum.FILTER_SET_PAGINATION:
      return a.filterSetPaginationReducer(state, action as a.FilterSetPagination);
    case a.CardsSearchActionEnum.UPDATE_CARD:
      return a.udpateCardReducer(state);
    case a.CardsSearchActionEnum.DELETE_CARD:
      return a.deleteCardReducer(state);
    default:
      return state;
  }
}
