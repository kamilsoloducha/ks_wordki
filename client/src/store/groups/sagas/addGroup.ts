import { call, put } from '@redux-saga/core/effects'
import { requestFailed } from 'store/root/actions'
import * as api from 'api/index'
import { ApiResponse } from 'common/models/response'
import { takeEvery } from 'redux-saga/effects'
import { SagaIterator } from 'redux-saga'
import { PayloadAction } from '@reduxjs/toolkit'
import { AddGroup } from '../action-payloads'
import { getGroupsSummary } from '../reducer'

export function* addGroupWorker(action: PayloadAction<AddGroup>): any {
  const request = {
    name: action.payload.group.name,
    back: action.payload.group.back + '',
    front: action.payload.group.front + ''
  } as api.AddGroupRequest
  const { data, error }: { data: ApiResponse<string>; error: any } = yield call(
    api.addGroup,
    request
  )
  yield put(data ? getGroupsSummary() : requestFailed(error))
}

export function* addGroupEffect(): SagaIterator {
  yield takeEvery('groups/addGroup', addGroupWorker)
}
