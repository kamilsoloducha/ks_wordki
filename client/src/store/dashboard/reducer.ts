import { DashboardAction, DashboardActionEnum } from "./actions";
import DashboardState, { initailState } from "./state";

export default function dashboardReducer(
  state = initailState,
  action: DashboardAction
): DashboardState {
  switch (action.type) {
    case DashboardActionEnum.GET_DASHBAORD_SUMMARY:
      return action.reduce(state);
    case DashboardActionEnum.GET_DASHBAORD_SUMMARY_SUCCESS:
      return action.reduce(state);
    default:
      return state;
  }
}
