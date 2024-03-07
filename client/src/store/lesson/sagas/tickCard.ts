import { call, select, takeEvery } from '@redux-saga/core/effects'
import * as api from 'api/index'
import { selectCurrectRepeat } from '../selectors'
import { Repeat } from 'pages/lesson/models/repeat'
import { SagaIterator } from 'redux-saga'

export function* tickCardWorker(): SagaIterator {
  yield takeEvery('lesson/tickCard', tickCard)
}

export function* tickCard(): SagaIterator {
  const repeat: Repeat = yield select(selectCurrectRepeat)
  const response: { isCorrect: boolean } = yield call(api.tickCard, repeat.cardId)
  if (!response.isCorrect) {
    console.error('Error occured')
  }
}
