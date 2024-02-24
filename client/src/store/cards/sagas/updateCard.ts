import { call, put } from '@redux-saga/core/effects'
import { requestFailed } from 'store/root/actions'
import * as api from 'api/index'
import { SagaIterator } from 'redux-saga'
import { takeEvery } from 'redux-saga/effects'
import { PayloadAction } from '@reduxjs/toolkit'
import { UpdateCard } from '../action-payload'
import { applyFilters, updateCardSuccess } from '../reducer'

export function* updateCardWorker(action: PayloadAction<UpdateCard>): any {
  const response: {} | boolean = yield call(api.updateCard, action.payload.card)
  if (response !== false) {
    yield put(updateCardSuccess({ card: action.payload.card }))
    yield put(applyFilters())
  } else {
    yield put(requestFailed({} as any))
  }
}

export function* updateCardEffect(): SagaIterator {
  yield takeEvery('cards/updateCard', updateCardWorker)
}
