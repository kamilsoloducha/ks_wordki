import { Action } from "redux";
import DashboardState from "./state";

export enum DashboardActionEnum {
  GET_DASHBAORD_SUMMARY = "[DASHBOARD] GET_DASHBAORD_SUMMARY",
  GET_DASHBAORD_SUMMARY_SUCCESS = "[DASHBOARD] GET_DASHBAORD_SUMMARY_SUCCESS",

  GET_FORECAST = "[DASHBOARD] GET_FORECAST",
  GET_FORECAST_SUCCESS = "[DASHBOARD] GET_FORECAST_SUCCESS",
}

export interface DashboardAction {
  type: DashboardActionEnum;
  reduce: (state: DashboardState) => DashboardState;
}

export interface GetDashboardSummary extends DashboardAction {}

export function getDashboardSummary(): GetDashboardSummary {
  return {
    type: DashboardActionEnum.GET_DASHBAORD_SUMMARY,
    reduce: (state: DashboardState): DashboardState => {
      return { ...state, isLoading: true };
    },
  };
}

export interface GetDashboardSummarySuccess extends DashboardAction {
  dailyRepeats: number;
  groupsCount: number;
  cardsCount: number;
}

export function getDashboardSummarySuccess(
  dailyRepeats: number,
  groupsCount: number,
  cardsCount: number
): GetDashboardSummarySuccess {
  return {
    type: DashboardActionEnum.GET_DASHBAORD_SUMMARY_SUCCESS,
    reduce: (state: DashboardState): DashboardState => {
      return {
        ...state,
        isLoading: false,
        dailyRepeats: dailyRepeats,
        groupsCount: groupsCount,
        cardsCount: cardsCount,
      };
    },
    cardsCount: cardsCount,
    dailyRepeats: dailyRepeats,
    groupsCount: groupsCount,
  };
}

export function getForecast(): Action {
  return {
    type: DashboardActionEnum.GET_FORECAST,
  };
}
export function reduceGetForecast(state: DashboardState): DashboardState {
  return {
    ...state,
    isForecastLoading: true,
  };
}

export interface GetForecastSuccess extends Action {
  forecast: any[];
}
export function getForecastSuccess(forecast: any[]): GetForecastSuccess {
  return {
    type: DashboardActionEnum.GET_FORECAST_SUCCESS,
    forecast,
  };
}
export function reduceGetForecastSuccess(
  state: DashboardState,
  action: GetForecastSuccess
): DashboardState {
  return {
    ...state,
    forecast: action.forecast,
    isForecastLoading: false,
  };
}
