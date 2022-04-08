import { call, put, select, take } from "@redux-saga/core/effects";
import { ForecastModel } from "pages/dashboard/models/forecastModel";
import { requestFailed } from "store/root/actions";
import { selectUserId } from "store/user/selectors";
import { DashboardActionEnum, getDashboardSummarySuccess, getForecastSuccess } from "../actions";
import * as api from "api";
import { SagaIterator } from "redux-saga";

export function* getDashbaordSummaryEffect(): SagaIterator {
  yield take(DashboardActionEnum.GET_DASHBAORD_SUMMARY);
  const userId: string = yield select(selectUserId);
  const { data, error }: { data: api.DashboardSummaryResponse; error: any } = yield call(
    api.getDashboardSummaryApi,
    userId
  );

  const tomorrow = new Date(Date.now() + 1000 * 60 * 60 * 24);
  const getForecastRequest: api.ForecastQuery = {
    count: 7,
    ownerId: userId,
    startDate: tomorrow,
  };

  const result: ForecastModel[] = yield call(api.getForecast, getForecastRequest);
  yield put(
    data
      ? getDashboardSummarySuccess(data.dailyRepeats, data.groupsCount, data.cardsCount)
      : requestFailed(error)
  );
  yield put(getForecastSuccess(result));
}
