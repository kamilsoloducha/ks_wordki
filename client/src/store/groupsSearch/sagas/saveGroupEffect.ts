import * as api from 'api/index'
import { call, put, select, take } from '@redux-saga/core/effects'
import { selectSelectedGroup } from '../selectors'
import { GroupSummary } from 'pages/groupsSearch/models/groupSummary'
import { selectUserId } from 'store/user/selectors'
import { SagaIterator } from 'redux-saga'
import { resetSelection, saveGroupSuccess } from '../reducer'

export function* saveGroupEffect(): SagaIterator {
  while (true) {
    yield take('groupsSearch/saveGroup')
    const ownerId: string = yield select(selectUserId)
    const selectedGroup: GroupSummary = yield select(selectSelectedGroup)

    const request: api.SaveGroupRequest = {
      ownerId,
      groupId: selectedGroup.id
    }

    yield call(api.saveGroup, request)

    yield put(saveGroupSuccess())
    yield put(resetSelection())
  }
}
