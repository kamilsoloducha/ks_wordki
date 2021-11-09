import { Model } from "./state";

export enum ActionEnum {
  GET_SUMMARY = "GET_SUMMARY",
  GET_SUMMARY_SUCCESS = "GET_SUMMARY_SUCCESS",
}

export class GetSummary {
  readonly type = ActionEnum.GET_SUMMARY;

  reduce(state: Model): Model {
    return { ...state, isLoading: true };
  }
}

export class GetSummarySuccess {
  readonly type = ActionEnum.GET_SUMMARY_SUCCESS;

  constructor(
    private readonly groupsCount: number,
    private readonly cardsNumber: number,
    private readonly dailyRepeats: number
  ) {}

  reduce(state: Model): Model {
    return {
      ...state,
      groupsCount: this.groupsCount,
      cardsCount: this.cardsNumber,
      dailyRepeats: this.dailyRepeats,
      isLoading: false,
    };
  }
}

export type Actions = GetSummary | GetSummarySuccess;
