import { MainState } from 'store/store'

export const selectFilter = (state: MainState) => state.cardsSeachReducer.filter
export const selectIsSearching = (state: MainState) => state.cardsSeachReducer.isSearching
export const selectCards = (state: MainState) => state.cardsSeachReducer.cards
export const selectCardsCount = (state: MainState) => state.cardsSeachReducer.cardsCount
export const selectOverview = (state: MainState) => state.cardsSeachReducer.overview
