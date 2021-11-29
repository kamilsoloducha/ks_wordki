import { call, select, takeLatest } from "@redux-saga/core/effects";
import { selectUserId } from "store/user/selectors";
import { Correct, DailyActionEnum, Wrong } from "../actions";
import * as api from "pages/lesson/services/repeatsApi";
import { selectShouldSendAnswer } from "../selectors";

function* answer(action: Correct | Wrong) {
  const shouldUpdate: boolean = yield select(selectShouldSendAnswer);
  if (!shouldUpdate) return;
  console.log(shouldUpdate);

  const userId: string = yield select(selectUserId);
  yield call(() =>
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
