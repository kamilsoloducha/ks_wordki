import * as actions from "../actions";
import * as api from "api";
import { call, put, select, takeLatest } from "@redux-saga/core/effects";
import { selectSelectedGroup } from "../selectors";
import { GroupSummary } from "pages/groupsSearch/models/groupSummary";
import { CardSummary } from "pages/groupsSearch/models/cardSummary";

function* getCards() {
  const selectedGroup: GroupSummary = yield select(selectSelectedGroup);

  const cards: CardSummary[] = yield call(api.getCards, selectedGroup.id);

  yield put(actions.getCardsSuccess(cards));
}

export function* getCardsEffect() {
  yield takeLatest(actions.GroupsSearchActionEnum.GET_CARDS, getCards);
}
