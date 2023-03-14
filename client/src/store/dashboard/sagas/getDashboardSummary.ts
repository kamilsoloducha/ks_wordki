import { call, put, select } from "@redux-saga/core/effects";
import { ForecastModel } from "pages/dashboard/models/forecastModel";
import { requestFailed } from "store/root/actions";
import * as api from "api";
import { SagaIterator } from "redux-saga";
import { getDashboardSummarySuccess, getForecastSuccess } from "../reducer";
import { takeEvery } from "redux-saga/effects";

export function* getDashbaordSummaryWorker(): any {
  const response: { data: api.DashboardSummaryResponse; error: any } = yield call(
    api.getDashboardSummaryApi
  );

  const getForecastRequest: api.ForecastQuery = {
    count: 7,
  };

  const result: ForecastModel[] = yield call(api.getForecast, getForecastRequest);
  yield put(
    response.data
      ? getDashboardSummarySuccess({
          dailyRepeats: response.data.dailyRepeats,
          groupsCount: response.data.groupsCount,
          cardsCount: response.data.cardsCount,
        })
      : requestFailed(response.error)
  );
  yield put(getForecastSuccess({ forecast: result }));
}

export function* getDashbaordSummaryEffect(): SagaIterator {
  yield takeEvery("dashboard/getDashboardSummary", getDashbaordSummaryWorker);
}
