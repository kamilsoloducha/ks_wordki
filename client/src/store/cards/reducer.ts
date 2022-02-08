import { Action } from "redux";
import * as actions from "./actions";
import CardsState, { initialState } from "./state";

export default function cardsReducer(state = initialState, action: Action): CardsState {
  switch (action.type) {
    case actions.CardsActionEnum.ADD_CARD:
      return actions.reduceAddCard(state);
    case actions.CardsActionEnum.ADD_CARD_SUCCESS:
      return actions.reduceAddCardSuccess(state);
    case actions.CardsActionEnum.APPEND_CARD:
      return actions.reduceAppendCard(state);
    case actions.CardsActionEnum.DELETE_CARD:
      return actions.reduceDeleteCard(state);
    case actions.CardsActionEnum.DELETE_CARD_SUCCESS:
      return actions.reduceDeleteCardSuccess(state, action as actions.DeleteCardSuccess);
    case actions.CardsActionEnum.GET_CARDS:
      return actions.reduceGetCards(state);
    case actions.CardsActionEnum.GET_CARDS_SUCCESS:
      return actions.reduceGetCardsSuccess(state, action as actions.GetCardsSuccess);
    case actions.CardsActionEnum.RESET_SELECTED_CARD:
      return actions.reduceResetSelectedCard(state);
    case actions.CardsActionEnum.SELECT_CARD:
      return actions.reduceSelectCard(state, action as actions.SelectCard);
    case actions.CardsActionEnum.UPDATE_CARD:
      return actions.reduceUpdateCard(state, action as actions.UpdateCard);
    case actions.CardsActionEnum.UPDATE_CARD_SUCCESS:
      return actions.reduceUpdateCardSuccess(state, action as actions.UpdateCardSuccess);
    case actions.CardsActionEnum.SET_FILTER_DRAWER:
      return actions.reduceSetFilterDrawer(state, action as actions.SetFilterDrawer);
    case actions.CardsActionEnum.SET_FILTER_LEARNING:
      return actions.reduceSetFilterLearning(state, action as actions.SetFilterLearning);
    case actions.CardsActionEnum.SET_FILTER_TEXT:
      return actions.reduceSetFilterText(state, action as actions.SetFilterText);
    case actions.CardsActionEnum.RESET_FILTERS:
      return actions.reduceResetFilter(state);
    case actions.CardsActionEnum.SET_FILTERED_CARDS:
      return actions.reduceSetFilteredCards(state, action as actions.SetFilteredCards);
    case actions.CardsActionEnum.APPLY_FILTERS:
      return actions.reduceApplyFilters(state);
    default:
      return state;
  }
}
