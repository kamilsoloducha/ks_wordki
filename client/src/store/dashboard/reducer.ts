import {
  DashboardAction,
  DashboardActionEnum,
  reduceGetForecast,
  reduceGetForecastSuccess,
} from "./actions";
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
    case DashboardActionEnum.GET_FORECAST:
      return reduceGetForecast(state);
    case DashboardActionEnum.GET_FORECAST_SUCCESS:
      return reduceGetForecastSuccess(state, action as any);
    default:
      return state;
  }
}
