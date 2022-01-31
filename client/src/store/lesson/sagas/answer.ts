import { call, select, takeLatest } from "@redux-saga/core/effects";
import { selectUserId } from "store/user/selectors";
import { Correct, DailyActionEnum, Wrong } from "../actions";
import * as api from "pages/lesson/services/repeatsApi";
import { selectShouldSendAnswer } from "../selectors";
import { RegisterAnswerRequest } from "pages/lesson/requests";

function* answer(action: Correct | Wrong) {
  const shouldUpdate: boolean = yield select(selectShouldSendAnswer);
  if (!shouldUpdate) return;

  const userId: string = yield select(selectUserId);
  yield call(() =>
    api.registerAnswer({
      userId,
      sideId: action.sideId,
      result: action.result,
    } as RegisterAnswerRequest)
  );
}

export function* correctEffect() {
  yield takeLatest(DailyActionEnum.LESSON_CORRECT, answer);
}

export function* wrongEffect() {
  yield takeLatest(DailyActionEnum.LESSON_WRONG, answer);
}
