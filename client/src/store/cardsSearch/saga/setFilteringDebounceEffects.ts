import { SagaIterator } from 'redux-saga'
import { debounce, put } from 'redux-saga/effects'
import { search } from '../reducer'

export function* setSearchingTermEffect(): SagaIterator {
  yield debounce(1000, 'cardsSearch/filterSetTerm', searchWorker)
}

export function* searchWorker(): SagaIterator {
  yield put(search())
}
