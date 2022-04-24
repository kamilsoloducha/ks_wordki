import { call, select, takeLatest } from "@redux-saga/core/effects";
import { selectUserId } from "store/user/selectors";
import * as api from "api";
import { selectLessonHistory, selectShouldSendAnswer } from "../selectors";
import UserRepeat from "pages/lesson/models/userRepeat";
import { Correct, Wrong } from "../action-payloads";
import { PayloadAction } from "@reduxjs/toolkit";

export function* answer(action: PayloadAction<Correct> | PayloadAction<Wrong>) {
  const shouldUpdate: boolean = yield select(selectShouldSendAnswer);
  if (!shouldUpdate) return;

  const userId: string = yield select(selectUserId);
  const userRepeats: UserRepeat[] = yield select(selectLessonHistory);
  const previousRepeat = userRepeats[userRepeats.length - 1];
  yield call(() =>
    api.registerAnswer({
      userId,
      sideId: previousRepeat.repeat.sideId,
      result: action.payload.result,
    } as api.RegisterAnswerRequest)
  );
}

export function* correctEffect() {
  yield takeLatest("lesson/correct", answer);
}

export function* wrongEffect() {
  yield takeLatest("lesson/wrong", answer);
}
