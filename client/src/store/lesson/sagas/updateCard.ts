import { call, put, select, takeLatest } from "@redux-saga/core/effects";
import { requestFailed } from "store/root/actions";
import { selectUserId } from "store/user/selectors";
import * as api from "pages/cards/services/groupDetailsApi";
import { DailyActionEnum, UpdateCard, updateCardSuccess } from "../actions";
import { UpdateCardRequest } from "pages/cards/models/requests/updateCardRequest";

function* updateCard(action: UpdateCard) {
  const userId: string = yield select(selectUserId);

  const request = {
    userId,
    groupId: action.groupId,
    cardId: action.form.cardId,
    front: {
      value: action.form.frontValue,
      example: action.form.frontExample,
      isUsed: action.form.frontEnabled,
      isTicked: action.form.isTicked,
    },
    back: {
      value: action.form.backValue,
      example: action.form.backExample,
      isUsed: action.form.backEnabled,
      isTicked: action.form.isTicked,
    },
  } as UpdateCardRequest;

  const { data, error }: { data: {}; error: any } = yield call(() => api.updateCard2(request));
  yield put(data ? updateCardSuccess(action.form) : requestFailed(error));
}

export function* updateCardEffect() {
  yield takeLatest(DailyActionEnum.UPDATE_CARD, updateCard);
}
