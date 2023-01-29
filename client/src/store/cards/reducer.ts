import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { CardSummary } from "pages/cards/models";
import * as payloads from "./action-payload";
import CardsState, { initialState } from "./state";

export const cardsSlice = createSlice({
  name: "cards",
  initialState: initialState,
  reducers: {
    addCard: (_: CardsState, __: PayloadAction<payloads.AddCard>): void => {},
    addCardSuccess: (state: CardsState, _: PayloadAction<payloads.AddCardSuccess>): void => {
      state.selectedItem = null;
    },
    deleteCard: (_: CardsState, __: PayloadAction<payloads.DeleteCard>): void => {},
    deleteCardSuccess: (
      state: CardsState,
      action: PayloadAction<payloads.DeleteCardSuccess>
    ): void => {
      state.cards = state.cards.filter((item) => item.id !== action.payload.cardId);
    },
    getCard: (state: CardsState, _: PayloadAction<payloads.GetCard>): void => {},
    getCardSuccess: (state: CardsState, action: PayloadAction<CardSummary>): void => {
      state.cards.push(action.payload);
    },
    getCards: (state: CardsState, _: PayloadAction<payloads.GetCards>): void => {
      state.isLoading = true;
    },
    getCardsSuccess: (state: CardsState, action: PayloadAction<payloads.GetCardsSuccess>): void => {
      state.id = action.payload.id;
      state.name = action.payload.name;
      state.language1 = action.payload.language1;
      state.language2 = action.payload.language2;
      state.cards = action.payload.cards;
      state.isLoading = false;
      state.selectedItem = null;
    },
    resetSelectedCard: (state: CardsState): void => {
      state.selectedItem = null;
    },
    selectCard: (state: CardsState, action: PayloadAction<payloads.SelectCard>): void => {
      state.selectedItem = action.payload.item;
    },
    updateCard: (state: CardsState, _: PayloadAction<payloads.UpdateCard>): void => {},
    updateCardSuccess: (
      state: CardsState,
      action: PayloadAction<payloads.UpdateCardSuccess>
    ): void => {
      const index = state.cards.findIndex((item) => item.id === action.payload.card.id);
      state.cards[index] = action.payload.card;
      state.selectedItem = null;
    },
    setFilterDrawer: (state: CardsState, action: PayloadAction<payloads.SetFilterDrawer>): void => {
      state.filter.drawer = action.payload.drawer;
    },
    setFilterLearning: (
      state: CardsState,
      action: PayloadAction<payloads.SetFilterLearning>
    ): void => {
      state.filter.isLearning = action.payload.isLearning;
    },
    setFilterText: (state: CardsState, action: PayloadAction<payloads.SetFilterText>): void => {
      state.filter.text = action.payload.text;
    },
    setFilterIsTicked: (
      state: CardsState,
      action: PayloadAction<payloads.SetFitlerIsTicked>
    ): void => {
      state.filter.isTicked = action.payload.isTicked;
    },
    setFilteredCards: (
      state: CardsState,
      action: PayloadAction<payloads.SetFilteredCards>
    ): void => {
      state.filteredCards = action.payload.cards;
    },
    resetFilter: (state: CardsState): void => {
      state.filter = initialState.filter;
    },
    applyFilters: (): void => {},
  },
});

export default cardsSlice.reducer;

export const {
  addCard,
  addCardSuccess,
  deleteCard,
  deleteCardSuccess,
  getCard,
  getCardSuccess,
  getCards,
  getCardsSuccess,
  updateCard,
  updateCardSuccess,
  selectCard,
  resetSelectedCard,
  setFilterDrawer,
  setFilterIsTicked,
  setFilterLearning,
  setFilterText,
  setFilteredCards,
  resetFilter,
  applyFilters,
} = cardsSlice.actions;
