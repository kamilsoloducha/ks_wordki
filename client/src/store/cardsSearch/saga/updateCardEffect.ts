import { call, put, select, takeLatest } from "@redux-saga/core/effects";
import * as actions from "../actions";
import * as api from "api";
import { selectUserId } from "store/user/selectors";
import { take } from "redux-saga/effects";

export function* updateCardEffect() {
  const action: actions.UpdateCard = yield take(actions.CardsSearchActionEnum.UPDATE_CARD);
  const userId: string = yield select(selectUserId);
  const request: api.UpdateCardRequest = {
    userId: userId,
    groupId: action.card.groupId,
    cardId: action.card.id,
    front: {
      value: action.card.front.value,
      example: action.card.front.example,
      isUsed: action.card.front.isUsed,
      isTicked: action.card.front.isTicked,
    },
    back: {
      value: action.card.back.value,
      example: action.card.back.example,
      isUsed: action.card.back.isUsed,
      isTicked: action.card.back.isTicked,
    },
  };
  yield call(api.updateCard2, request);
  yield put(actions.search());
}
