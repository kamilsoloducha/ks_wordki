import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import DashboardState, { initailState } from "./state";
import * as p from "./action-payloads";

export const dashbaordSlice = createSlice({
  name: "dashboard",
  initialState: initailState,
  reducers: {
    getDashboardSummary: (state: DashboardState): void => {
      state.isLoading = true;
    },
    getDashboardSummarySuccess: (
      state: DashboardState,
      action: PayloadAction<p.GetDashboardSummarySuccess>
    ): void => {
      state.isLoading = false;
      state.groupsCount = action.payload.groupsCount;
      state.cardsCount = action.payload.cardsCount;
      state.dailyRepeats = action.payload.dailyRepeats;
    },

    getForecast: (state: DashboardState): void => {
      state.isForecastLoading = true;
    },
    getForecastSuccess: (
      state: DashboardState,
      action: PayloadAction<p.GetForecastSuccess>
    ): void => {
      state.isForecastLoading = false;
      state.forecast = action.payload.forecast;
    },
  },
});

export default dashbaordSlice.reducer;

export const { getDashboardSummary, getDashboardSummarySuccess, getForecast, getForecastSuccess } =
  dashbaordSlice.actions;
