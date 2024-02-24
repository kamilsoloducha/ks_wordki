import { call, put } from '@redux-saga/core/effects'
import { ForecastModel } from 'pages/dashboard/models/forecastModel'
import { requestFailed } from 'store/root/actions'
import * as api from 'api/index'
import { SagaIterator } from 'redux-saga'
import { getDashboardSummarySuccess, getForecastSuccess } from '../reducer'
import { takeEvery } from 'redux-saga/effects'

export function* getDashbaordSummaryWorker(): any {
  const response: api.DashboardSummaryResponse = yield call(api.getDashboardSummaryApi)

  const getForecastRequest: api.ForecastQuery = {
    count: 7
  }

  const result: ForecastModel[] = yield call(api.getForecast, getForecastRequest)
  yield put(
    response
      ? getDashboardSummarySuccess({
          dailyRepeats: response.dailyRepeats,
          groupsCount: response.groupsCount,
          cardsCount: response.cardsCount
        })
      : requestFailed(response)
  )
  yield put(getForecastSuccess({ forecast: result }))
}

export function* getDashbaordSummaryEffect(): SagaIterator {
  yield takeEvery('dashboard/getDashboardSummary', getDashbaordSummaryWorker)
}
