import { call, put } from '@redux-saga/core/effects'
import { requestFailed } from 'store/root/actions'
import * as api from 'api/index'
import { ApiResponse } from 'common/models/response'
import { SagaIterator } from 'redux-saga'
import { getCards } from 'store/cards/reducer'
import { PayloadAction } from '@reduxjs/toolkit'
import { UpdateGroup } from '../action-payloads'
import { takeEvery } from 'redux-saga/effects'

export function* updateGroupWorker(action: PayloadAction<UpdateGroup>): any {
  const request = {
    name: action.payload.group.name,
    back: action.payload.group.back,
    front: action.payload.group.front
  } as api.UpdateGroupRequest
  const { error }: { data: ApiResponse<string>; error: any } = yield call(
    api.updateGroup,
    action.payload.group.id!,
    request
  )
  yield put(error ? requestFailed(error) : getCards({ groupId: action.payload.group.id! }))
}

export function* updateGroupEffect(): SagaIterator {
  yield takeEvery('groups/updateGroup', updateGroupWorker)
}
