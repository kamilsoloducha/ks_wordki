import { Action } from "@reduxjs/toolkit";
import DashboardState, { initailState } from "../state";
import * as actions from "../reducer";

interface Context {
  givenState: DashboardState;
  action: Action;
  expectedResult: DashboardState;
}

export class GetDashboardSummaryCtx implements Context {
  givenState = { ...initailState };
  action = actions.getDashboardSummary();
  expectedResult: DashboardState = { ...initailState, isLoading: true };
}

export class GetDashboardSummarySuccessCtx implements Context {
  givenState: DashboardState = { ...initailState, isLoading: true };
  action = actions.getDashboardSummarySuccess({ cardsCount: 1, dailyRepeats: 2, groupsCount: 3 });
  expectedResult: DashboardState = {
    ...initailState,
    isLoading: false,
    cardsCount: 1,
    dailyRepeats: 2,
    groupsCount: 3,
  };
}

export class GetForecastCtx implements Context {
  givenState = { ...initailState };
  action = actions.getForecast();
  expectedResult: DashboardState = { ...initailState, isForecastLoading: true };
}

export class GetForecastSuccessCtx implements Context {
  givenState: DashboardState = { ...initailState, isForecastLoading: true };
  action = actions.getForecastSuccess({ forecast: [] });
  expectedResult: DashboardState = { ...initailState, isForecastLoading: false, forecast: [] };
}
