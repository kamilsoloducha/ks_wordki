import { GroupsAction } from "./actions";
import GroupsState, { initialState } from "./state";

export default function groupsReducer(
  state = initialState,
  action: GroupsAction
): GroupsState {
  return action.type.includes("[GROUPS]") ? action.reduce(state) : state;
}
