import { call, select, takeLatest } from '@redux-saga/core/effects'
import * as api from 'api/index'
import { selectLessonHistory, selectShouldSendAnswer } from '../selectors'
import UserRepeat from 'pages/lesson/models/userRepeat'
import { Correct, Wrong } from '../action-payloads'
import { PayloadAction } from '@reduxjs/toolkit'

export function* answer(action: PayloadAction<Correct> | PayloadAction<Wrong>) {
  const shouldUpdate: boolean = yield select(selectShouldSendAnswer)
  if (!shouldUpdate) return

  const userRepeats: UserRepeat[] = yield select(selectLessonHistory)
  const previousRepeat = userRepeats[userRepeats.length - 1]

  const request: api.RegisterAnswerRequest = {
    cardId: previousRepeat.repeat.cardId,
    sideType: previousRepeat.repeat.sideType,
    result: action.payload.result
  }
  yield call(api.registerAnswer, request)
}

export function* correctEffect() {
  yield takeLatest('lesson/correct', answer)
}

export function* wrongEffect() {
  yield takeLatest('lesson/wrong', answer)
}
