import { call, put } from '@redux-saga/core/effects'
import * as api from 'api/index'
import { CardsOverview } from 'pages/cardsSearch/models/cardsOverview'
import { takeEvery } from 'redux-saga/effects'
import { SagaIterator } from 'redux-saga'
import { getOverviewSuccess } from '../reducer'
import { useUserStorage } from 'common/index'

export function* getOverviewWorker(): SagaIterator {
  const { get } = useUserStorage()
  const userId = get()?.id!

  const overview: CardsOverview = yield call(api.cardsOverview, userId)

  yield put(getOverviewSuccess({ overview }))
}

export function* getOverviewEffect(): SagaIterator {
  yield takeEvery('cardsSearch/getOverview', getOverviewWorker)
}
