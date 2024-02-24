import { call, put, takeEvery } from '@redux-saga/core/effects'
import * as api from 'api/index'
import { SagaIterator } from 'redux-saga'
import { PayloadAction } from '@reduxjs/toolkit'
import { UpdateCard } from '../action-payloads'
import { search } from '../reducer'

export function* updateCardEffect(): SagaIterator {
  yield takeEvery('cardsSearch/updateCard', updateCardWorker)
}

export function* updateCardWorker(action: PayloadAction<UpdateCard>): SagaIterator {
  yield call(api.updateCard, action.payload.card)
  yield put(search())
}
