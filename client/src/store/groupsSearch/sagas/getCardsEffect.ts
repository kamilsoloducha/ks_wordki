import * as actions from "../actions";
import * as api from "api";
import { call, put, select, takeLatest } from "@redux-saga/core/effects";
import { selectSelectedGroup } from "../selectors";
import { GroupSummary } from "pages/groupsSearch/models/groupSummary";
import { CardSummary } from "pages/groupsSearch/models/cardSummary";
import { SagaIterator } from "redux-saga";
import { take } from "redux-saga/effects";

export function* getCardsEffect(): SagaIterator {
  yield take(actions.GroupsSearchActionEnum.GET_CARDS);
  const selectedGroup: GroupSummary = yield select(selectSelectedGroup);

  const cards: CardSummary[] = yield call(api.getCards, selectedGroup.id);

  yield put(actions.getCardsSuccess(cards));
}
