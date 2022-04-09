import { call, put, select } from "@redux-saga/core/effects";
import { requestFailed } from "store/root/actions";
import { selectUserId } from "store/user/selectors";
import { CardsActionEnum, selectCard, UpdateCard } from "../actions";
import { selectGroupId } from "../selectors";
import { addCard } from "api";
import { CardSummary } from "pages/cards/models";
import { SagaIterator } from "redux-saga";
import { take } from "redux-saga/effects";

export function* addCardEffect(): SagaIterator {
  const action: UpdateCard = yield take(CardsActionEnum.ADD_CARD);
  const userId: string = yield select(selectUserId);
  const id: string = yield select(selectGroupId);

  const response: string | boolean = yield call(addCard, userId, id, action.card);
  yield put(
    response !== false
      ? selectCard({ front: {}, back: {} } as CardSummary)
      : requestFailed({} as any)
  );
}
