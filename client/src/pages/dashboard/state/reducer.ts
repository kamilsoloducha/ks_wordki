import { Actions } from "./actions";
import { Model } from "./state";

export function reducer(state: Model, action: Actions) {
  return action.reduce(state);
}
