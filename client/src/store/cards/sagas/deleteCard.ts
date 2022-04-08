import { call, put, select } from "@redux-saga/core/effects";
import { requestFailed } from "store/root/actions";
import { selectUserId } from "store/user/selectors";
import { CardsActionEnum, deleteCardSuccess } from "../actions";
import { selectGroupId, selectSelectedCard } from "../selectors";
import * as api from "api";
import { ApiResponse } from "common/models/response";
import { CardSummary } from "pages/cards/models";
import { SagaIterator } from "redux-saga";
import { take } from "redux-saga/effects";

export function* deleteCardEffect(): SagaIterator {
  yield take(CardsActionEnum.DELETE_CARD);

  const userId: string = yield select(selectUserId);
  const groupId: string = yield select(selectGroupId);
  const selectedItem: CardSummary = yield select(selectSelectedCard);

  const { data, error }: { data: ApiResponse<string>; error: any } = yield call(() =>
    api.deleteCard(userId, groupId, selectedItem.id)
  );
  yield put(data.isCorrect ? deleteCardSuccess(selectedItem.id) : requestFailed(error));
}
