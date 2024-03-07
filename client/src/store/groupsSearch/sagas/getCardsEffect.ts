import * as api from 'api/index'
import { call, put, select } from '@redux-saga/core/effects'
import { selectSelectedGroup } from '../selectors'
import { CardSummary } from 'pages/groupsSearch/models/cardSummary'
import { SagaIterator } from 'redux-saga'
import { take } from 'redux-saga/effects'
import { getCardsSuccess } from '../reducer'
import { GroupSummary } from 'common/models/groupSummary'

export function* getCardsEffect(): SagaIterator {
  while (true) {
    yield take('groupsSearch/getCards')
    const selectedGroup: GroupSummary = yield select(selectSelectedGroup)

    const cards: CardSummary[] = yield call(api.getCards, selectedGroup.id)

    yield put(getCardsSuccess({ cards }))
  }
}
