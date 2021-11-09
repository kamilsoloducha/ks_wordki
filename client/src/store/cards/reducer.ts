import { CardsAction } from "./actions";
import CardsState, { initialState } from "./state";

export default function cardsReducer(
  state = initialState,
  action: CardsAction
): CardsState {
  return action.type.includes("[CARDS]") ? action.reduce(state) : state;
}
