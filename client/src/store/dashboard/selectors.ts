import { MainState } from "store/store";

export const selectIsLoading = (state: MainState) =>
  state.dashboardReducer.isLoading;
export const selectData = (state: MainState) => state.dashboardReducer;
