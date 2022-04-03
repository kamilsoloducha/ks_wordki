import { call, select, takeLatest } from "@redux-saga/core/effects";
import { selectUserId } from "store/user/selectors";
import { Correct, DailyActionEnum, Wrong } from "../actions";
import * as api from "api";
import { selectLessonHistory, selectShouldSendAnswer } from "../selectors";
import UserRepeat from "pages/lesson/models/userRepeat";

function* answer(action: Correct | Wrong) {
  const shouldUpdate: boolean = yield select(selectShouldSendAnswer);
  if (!shouldUpdate) return;

  const userId: string = yield select(selectUserId);
  const userRepeats: UserRepeat[] = yield select(selectLessonHistory);
  const previousRepeat = userRepeats[userRepeats.length - 1];
  yield call(() =>
    api.registerAnswer({
      userId,
      sideId: previousRepeat.repeat.sideId,
      result: action.result,
    } as api.RegisterAnswerRequest)
  );
}

export function* correctEffect() {
  yield takeLatest(DailyActionEnum.LESSON_CORRECT, answer);
}

export function* wrongEffect() {
  yield takeLatest(DailyActionEnum.LESSON_WRONG, answer);
}
