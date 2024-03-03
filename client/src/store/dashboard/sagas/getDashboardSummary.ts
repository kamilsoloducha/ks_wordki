import { call, put } from '@redux-saga/core/effects'
import { ForecastModel } from 'pages/dashboard/models/forecastModel'
import * as api from 'api/index'
import { SagaIterator } from 'redux-saga'
import { getDashboardSummarySuccess, getForecastSuccess } from '../reducer'
import { all, takeEvery } from 'redux-saga/effects'
import { AxiosResponse } from 'axios'
import { GetDashboardSummarySuccess, GetForecastSuccess } from 'store/dashboard/action-payloads'

export function* getDashbaordSummaryWorker(): any {
  const {
    summaryResponse,
    forecastResponse
  }: {
    summaryResponse: AxiosResponse<api.DashboardSummary>
    forecastResponse: AxiosResponse<ForecastModel[]>
  } = yield all({
    summaryResponse: call(api.getSummary),
    forecastResponse: call(api.getForecast, { count: 7 })
  })

  let summaryPayload: GetDashboardSummarySuccess
  if (summaryResponse && 'data' in summaryResponse) {
    summaryPayload = {
      cardsCount: summaryResponse.data.cardsCount,
      dailyRepeats: summaryResponse.data.dailyRepeats,
      groupsCount: summaryResponse.data.groupsCount
    }
  } else {
    summaryPayload = {
      cardsCount: 0,
      dailyRepeats: 0,
      groupsCount: 0
    }
  }
  yield put(getDashboardSummarySuccess(summaryPayload))

  let forecastPayload: GetForecastSuccess
  if (forecastResponse && 'data' in forecastResponse) {
    forecastPayload = {
      forecast: forecastResponse.data
    }
  } else {
    forecastPayload = {
      forecast: []
    }
  }
  yield put(getForecastSuccess(forecastPayload))
}

export function* getDashbaordSummaryEffect(): SagaIterator {
  yield takeEvery('dashboard/getDashboardSummary', getDashbaordSummaryWorker)
}
