import { call, put } from '@redux-saga/core/effects'
import { requestFailed } from 'store/root/actions'
import * as api from 'api/index'
import { SagaIterator } from 'redux-saga'
import { takeEvery } from 'redux-saga/effects'
import { getGroupsSummarySuccess } from '../reducer'
import { GroupSummary } from 'common/models/groupSummary'

export function* getGroupsSummaryWorker(): any {
  const { data, error }: { data: GroupSummary[]; error: any } = yield call(api.summaries)
  yield put(data ? getGroupsSummarySuccess({ groups: data }) : requestFailed(error))
}

export function* getGroupsSummaryEffect(): SagaIterator {
  yield takeEvery('groups/getGroupsSummary', getGroupsSummaryWorker)
}
