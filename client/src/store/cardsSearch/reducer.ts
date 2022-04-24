import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { CardsSearchState, initialCardsSearchState } from "./state";
import * as p from "./action-payloads";

export const cardsSerachSlice = createSlice({
  name: "cardsSerach",
  initialState: initialCardsSearchState,
  reducers: {
    getOverview: (_: CardsSearchState): void => {},
    getOverviewSuccess: (
      state: CardsSearchState,
      action: PayloadAction<p.GetOverviewSuccess>
    ): void => {
      state.overview = action.payload.overview;
    },
    search: (state: CardsSearchState): void => {
      state.isSearching = true;
    },
    searchSuccess: (state: CardsSearchState, action: PayloadAction<p.SeachSuccess>): void => {
      state.isSearching = false;
      state.cards = action.payload.cards;
      state.cardsCount = action.payload.cardsCount;
    },
    filterReset: (state: CardsSearchState): void => {
      state.filter = initialCardsSearchState.filter;
    },
    filterSetTerm: (state: CardsSearchState, action: PayloadAction<p.FilterSetTerm>): void => {
      state.filter.searchingTerm = action.payload.searchingTerm;
    },
    filterSetTickedOnly: (
      state: CardsSearchState,
      action: PayloadAction<p.FilterSetTickedOnly>
    ): void => {
      state.filter.tickedOnly = action.payload.tickedOnly;
    },
    filterSetLessonIncluded: (
      state: CardsSearchState,
      action: PayloadAction<p.FilterSetLessonIncluded>
    ): void => {
      state.filter.lessonIncluded = action.payload.lessonIncluded;
    },
    filterSetPagination: (
      state: CardsSearchState,
      action: PayloadAction<p.FilterSetPagination>
    ): void => {
      state.filter.pageNumber = action.payload.pageNumber;
      state.filter.pageSize = action.payload.pageSize;
    },
    updateCard: (__: CardsSearchState, _: PayloadAction<p.UpdateCard>): void => {},
    updateCardSuccess: (_: CardsSearchState, __: PayloadAction<p.UpdateCardSuccess>): void => {},
    deleteCard: (__: CardsSearchState, _: PayloadAction<p.DeleteCard>): void => {},
  },
});

export default cardsSerachSlice.reducer;

export const {
  deleteCard,
  filterReset,
  filterSetLessonIncluded,
  filterSetPagination,
  filterSetTerm,
  filterSetTickedOnly,
  getOverview,
  getOverviewSuccess,
  search,
  searchSuccess,
  updateCard,
  updateCardSuccess,
} = cardsSerachSlice.actions;
