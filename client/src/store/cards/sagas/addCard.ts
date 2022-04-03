import { call, put, select, takeLatest } from "@redux-saga/core/effects";
import { requestFailed } from "store/root/actions";
import { selectUserId } from "store/user/selectors";
import { CardsActionEnum, selectCard, UpdateCard } from "../actions";
import { selectGroupId } from "../selectors";
import { ApiResponse } from "common/models/response";
import { addCard } from "api";
import { CardSummary } from "pages/cards/models";

function* addCard2(action: UpdateCard) {
  const userId: string = yield select(selectUserId);
  const id: string = yield select(selectGroupId);

  const { data, error }: { data: ApiResponse<string>; error: any } = yield call(
    addCard,
    userId,
    id,
    action.card
  );
  yield put(data ? selectCard({ front: {}, back: {} } as CardSummary) : requestFailed(error));
}

export function* addCardEffect() {
  yield takeLatest(CardsActionEnum.ADD_CARD, addCard2);
}
