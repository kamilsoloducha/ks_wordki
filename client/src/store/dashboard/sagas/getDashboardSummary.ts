import { call, put, takeLatest, select } from "@redux-saga/core/effects";
import DashboardSummaryResponse from "pages/dashboard/models/dashboardSummaryResponse";
import { getDashboardSummaryApi } from "pages/dashboard/services/dashboardApi";
import { requestFailed } from "store/root/actions";
import { selectUserId } from "store/user/selectors";
import { DashboardActionEnum, GetDashboardSummary, getDashboardSummarySuccess } from "../actions";

function* getDashbaordSummary(_: GetDashboardSummary) {
  const userId: string = yield select(selectUserId);
  const { data, error }: { data: DashboardSummaryResponse; error: any } = yield call(
    getDashboardSummaryApi,
    userId
  );
  yield put(
    data
      ? getDashboardSummarySuccess(data.dailyRepeats, data.groupsCount, data.cardsCount)
      : requestFailed(error)
  );
}

export default function* getDashbaordSummaryEffect() {
  yield takeLatest(DashboardActionEnum.GET_DASHBAORD_SUMMARY, getDashbaordSummary);
}
