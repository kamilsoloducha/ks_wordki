import { call, put, takeLatest, select } from "@redux-saga/core/effects";
import DashboardSummaryResponse from "pages/dashboard/models/dashboardSummaryResponse";
import { ForecastModel } from "pages/dashboard/models/forecastModel";
import { GetForecastRequest } from "pages/dashboard/requests/getForecastRequest";
import { getDashboardSummaryApi, getForecast } from "pages/dashboard/services/dashboardApi";
import { requestFailed } from "store/root/actions";
import { selectUserId } from "store/user/selectors";
import {
  DashboardActionEnum,
  GetDashboardSummary,
  getDashboardSummarySuccess,
  getForecastSuccess,
} from "../actions";

function* getDashbaordSummary(_: GetDashboardSummary) {
  const userId: string = yield select(selectUserId);
  const { data, error }: { data: DashboardSummaryResponse; error: any } = yield call(
    getDashboardSummaryApi,
    userId
  );

  const tomorrow = new Date(Date.now() + 1000 * 60 * 60 * 24);
  const getForecastRequest: GetForecastRequest = { count: 7, ownerId: userId, startDate: tomorrow };

  const result: ForecastModel[] = yield call(getForecast, getForecastRequest);
  yield put(
    data
      ? getDashboardSummarySuccess(data.dailyRepeats, data.groupsCount, data.cardsCount)
      : requestFailed(error)
  );
  yield put(getForecastSuccess(result));
}

export default function* getDashbaordSummaryEffect() {
  yield takeLatest(DashboardActionEnum.GET_DASHBAORD_SUMMARY, getDashbaordSummary);
}
