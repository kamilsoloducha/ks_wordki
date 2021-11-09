import { call, put, takeLatest } from "@redux-saga/core/effects";
import http from "common/services/http/http";
import DashboardSummaryResponse from "pages/dashboard/models/dashboardSummaryResponse";
import { requestFailed } from "store/root/actions";
import {
  DashboardActionEnum,
  GetDashboardSummary,
  getDashboardSummarySuccess,
} from "../actions";

function fetchData() {
  return http
    .get<DashboardSummaryResponse>("/cards/dashboard/summary")
    .then((response) => ({ data: response.data }))
    .catch((error: Error) => ({ error }));
}

function* getDashbaordSummary(_: GetDashboardSummary) {
  const { data, error }: { data: DashboardSummaryResponse; error: any } =
    yield call(fetchData);
  yield put(
    data
      ? getDashboardSummarySuccess(
          data.dailyRepeats,
          data.groupsCount,
          data.cardsCount
        )
      : requestFailed(error)
  );
}

export default function* getDashbaordSummaryEffect() {
  yield takeLatest(
    DashboardActionEnum.GET_DASHBAORD_SUMMARY,
    getDashbaordSummary
  );
}
