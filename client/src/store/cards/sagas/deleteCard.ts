import { call, put, select } from '@redux-saga/core/effects'
import { requestFailed } from 'store/root/actions'
import { selectSelectedCard } from '../selectors'
import * as api from 'api/index'
import { CardSummary } from 'pages/cards/models'
import { SagaIterator } from 'redux-saga'
import { takeEvery } from 'redux-saga/effects'
import { applyFilters, deleteCardSuccess } from '../reducer'

export function* deleteCardWorker(): any {
  const selectedItem: CardSummary = yield select(selectSelectedCard)

  const response: {} | boolean = yield call(api.deleteCard, selectedItem.id)
  if (response !== false) {
    yield put(deleteCardSuccess({ cardId: selectedItem.id }))
    yield put(applyFilters())
  } else {
    yield put(requestFailed({} as any))
  }
}

export function* deleteCardEffect(): SagaIterator {
  yield takeEvery('cards/deleteCard', deleteCardWorker)
}
