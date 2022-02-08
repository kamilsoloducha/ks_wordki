import { MainState } from "store/store";

export const selectIsLoading = (state: MainState) => state.cardsReducer.isLoading;

export const selectCards = (state: MainState) => state.cardsReducer.cards;
export const selectGroupId = (state: MainState) => state.cardsReducer.id;
export const selectGroupDetails = (state: MainState) => {
  return {
    id: state.cardsReducer.id,
    name: state.cardsReducer.name,
    language1: state.cardsReducer.language1,
    language2: state.cardsReducer.language2,
  };
};
export const selectSelectedCard = (state: MainState) => state.cardsReducer.selectedItem;

export const selectFilterState = (state: MainState) => state.cardsReducer.filter;
export const selectFilteredCards = (state: MainState) => state.cardsReducer.filteredCards;
