import { call, select, takeLatest } from "@redux-saga/core/effects";
import { selectUserId } from "store/user/selectors";
import { Correct, DailyActionEnum, Wrong } from "../actions";
import * as api from "pages/lesson/services/repeatsApi";

function* answer(action: Correct | Wrong) {
  const userId: string = yield select(selectUserId);
  const { data }: { data: any; error: any } = yield call(() =>
    api.registerAnswer(
      userId,
      action.groupId,
      action.cardId,
      action.side,
      action.result
    )
  );
}

export function* correctEffect() {
  yield takeLatest(DailyActionEnum.LESSON_CORRECT, answer);
}

export function* wrongEffect() {
  yield takeLatest(DailyActionEnum.LESSON_WRONG, answer);
}