import * as api from "api";
import { call, put, select } from "@redux-saga/core/effects";
import { selectSelectedGroup } from "../selectors";
import { GroupSummary } from "pages/groupsSearch/models/groupSummary";
import { CardSummary } from "pages/groupsSearch/models/cardSummary";
import { SagaIterator } from "redux-saga";
import { take } from "redux-saga/effects";
import { getCardsSuccess } from "../reducer";

export function* getCardsEffect(): SagaIterator {
  yield take("groupsSearch/getCards");
  const selectedGroup: GroupSummary = yield select(selectSelectedGroup);

  const cards: CardSummary[] = yield call(api.getCards, selectedGroup.id);

  yield put(getCardsSuccess({ cards }));
}
