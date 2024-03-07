import * as api from 'api/index'
import { call, put, select, take } from '@redux-saga/core/effects'
import { selectSelectedGroup } from '../selectors'
import { selectUserId } from 'store/user/selectors'
import { SagaIterator } from 'redux-saga'
import { resetSelection, saveGroupSuccess } from '../reducer'
import { GroupSummary } from 'common/models/groupSummary'

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
