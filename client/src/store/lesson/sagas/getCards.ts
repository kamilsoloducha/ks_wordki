import * as api from 'api/index'
import { call, put, select } from '@redux-saga/core/effects'
import { selectUserId } from 'store/user/selectors'
import { selectLessonType, selectSettings } from '../selectors'
import { LessonSettings } from 'pages/lessonSettings/models/lessonSettings'
import history from 'common/services/history'
import { LessonMode } from 'pages/lessonSettings/models/lesson-mode'
import { SagaIterator } from 'redux-saga'
import { take } from 'redux-saga/effects'
import { getCardsSuccess } from '../reducer'
import { Repeat } from 'pages/lesson/models/repeat'
import { PayloadAction } from '@reduxjs/toolkit'
import { GetCards } from 'store/lesson/action-payloads'

export function* getCardsEffect(): SagaIterator {
  while (true) {
    const action: PayloadAction<GetCards> = yield take('lesson/getCards')
    const navigateFn = (location: string) => action.payload.navigate(location)
    const userId: string = yield select(selectUserId)
    const settings: LessonSettings = yield select(selectSettings)

    const getRepeatsRequest = prepareRequest(settings)

    const repeats: Repeat[] = yield call(api.repeats, getRepeatsRequest)

    if (repeats.length === 0) {
      yield call(navigateFn, '/dashboard')
      continue
    }

    const lessonType: number = yield select(selectLessonType)
    const startLessonRequest = { userId, lessonType } as api.StartLessonRequest

    yield call(api.startLesson, startLessonRequest)
    yield put(getCardsSuccess({ repeats }))
    yield call(navigateFn, '/lesson')
  }
}

function prepareRequest(settings: LessonSettings): api.RepeatsQuery {
  const request: api.RepeatsQuery = {
    count: settings.count,
    languages: settings.languages,
    groupId: settings.mode === LessonMode.Repetition ? null : settings.selectedGroupId,
    lessonIncluded: settings.mode === LessonMode.Repetition
  }
  return request
}

function forwardTo(location: any) {
  history.push(location)
}
