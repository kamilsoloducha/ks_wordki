import { call, put, select } from '@redux-saga/core/effects'
import { requestFailed } from 'store/root/actions'
import { selectGroupId } from '../selectors'
import { addCard } from 'api/index'
import { SagaIterator } from 'redux-saga'
import { takeEvery } from 'redux-saga/effects'
import { PayloadAction } from '@reduxjs/toolkit'
import { UpdateCard } from '../action-payload'
import { getCard, selectCard } from '../reducer'
import { CardSummary } from 'pages/cards/models'

export function* addCardWorker(action: PayloadAction<UpdateCard>): any {
  const groupId: string = yield select(selectGroupId)

  const response: string | boolean = yield call(addCard, groupId, action.payload.card)
  yield put(
    response !== false ? getCard({ groupId, cardId: response as string }) : requestFailed({} as any)
  )
  yield put(response !== false ? selectCard({ item: {} as CardSummary }) : requestFailed({} as any))
}

export function* addCardEffect(): SagaIterator {
  yield takeEvery('cards/addCard', addCardWorker)
}
