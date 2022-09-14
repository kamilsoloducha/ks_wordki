import "test/matcher/toDeepEqual";
import * as dashboard from "api/services/dashboard";
import * as api from "api";
import { SagaIterator } from "redux-saga";
import { call, put, select, take } from "redux-saga/effects";
import { getDashbaordSummaryEffect } from "../getDashboardSummary";
import { selectUserId } from "store/user/selectors";
import { DashboardSummaryResponse } from "api";
import { ForecastModel } from "pages/dashboard/models/forecastModel";
import { getDashboardSummarySuccess, getForecastSuccess } from "store/dashboard/reducer";

describe("getCardsEffect", () => {
  let saga: SagaIterator;
  let getDashboardMock: any;
  let getForecastMock: any;

  beforeEach(() => {
    getDashboardMock = jest.spyOn(dashboard, "getDashboardSummaryApi");
    getForecastMock = jest.spyOn(dashboard, "getForecast");
    // jest.useFakeTimers("modern");
    jest.setSystemTime(new Date(2022, 1, 2, 3, 4, 5));
    saga = getDashbaordSummaryEffect();
  });

  afterEach(() => {
    jest.restoreAllMocks();
  });

  it("should go through", () => {
    const userId = "userId";
    const getForecastRequest: api.ForecastQuery = {
      count: 7,
      ownerId: userId,
      startDate: new Date(2022, 1, 3, 3, 4, 5),
    };
    const getDashboardResponse = {
      data: {
        cardsCount: 1,
        dailyRepeats: 2,
        groupsCount: 3,
      },
    };
    const getForecastResponse: ForecastModel[] = [];

    expect(saga.next().value).toStrictEqual(take("dashboard/getDashboardSummary"));
    expect(saga.next().value).toStrictEqual(select(selectUserId));
    expect(saga.next(userId).value).toStrictEqual(call(getDashboardMock, userId));
    expect(saga.next(getDashboardResponse).value).toStrictEqual(
      call(getForecastMock, getForecastRequest)
    );
    expect(saga.next(getForecastResponse).value).toDeepEqual(
      put(
        getDashboardSummarySuccess({
          dailyRepeats: 2,
          groupsCount: 3,
          cardsCount: 1,
        })
      )
    );
    expect(saga.next().value).toDeepEqual(put(getForecastSuccess({ forecast: [] })));
  });
});
