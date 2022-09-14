import { call, put, select, take } from "@redux-saga/core/effects";
import { ForecastModel } from "pages/dashboard/models/forecastModel";
import { requestFailed } from "store/root/actions";
import { selectUserId } from "store/user/selectors";
import * as api from "api";
import { SagaIterator } from "redux-saga";
import { getDashboardSummarySuccess, getForecastSuccess } from "../reducer";
import { takeEvery } from "redux-saga/effects";

export function* getDashbaordSummaryEffect(): SagaIterator {
  yield takeEvery("dashboard/getDashboardSummary", function* () {
    const userId: string = yield select(selectUserId);
    const response: { data: api.DashboardSummaryResponse, error: any } = yield call(
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
      response.data
        ? getDashboardSummarySuccess({
            dailyRepeats: response.data.dailyRepeats,
            groupsCount: response.data.groupsCount,
            cardsCount: response.data.cardsCount,
          })
        : requestFailed(response.error)
    );
    yield put(getForecastSuccess({ forecast: result }));
  });
  
}
