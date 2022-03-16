import { call, put, select, takeLatest } from "@redux-saga/core/effects";
import * as actions from "../actions";
import * as api from "pages/cards/services/groupDetailsApi";
import { selectUserId } from "store/user/selectors";
import { UpdateCardRequest } from "pages/cards/models/requests/updateCardRequest";

function* updateCard(action: actions.UpdateCard) {
  const userId: string = yield select(selectUserId);
  const request: UpdateCardRequest = {
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

export function* updateCardEffect() {
  yield takeLatest(actions.CardsSearchActionEnum.UPDATE_CARD, updateCard);
}
