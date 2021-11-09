import DashboardState from "./state";

export enum DashboardActionEnum {
  GET_DASHBAORD_SUMMARY = "[DASHBOARD] GET_DASHBAORD_SUMMARY",
  GET_DASHBAORD_SUMMARY_SUCCESS = "[DASHBOARD] GET_DASHBAORD_SUMMARY_SUCCESS",
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
