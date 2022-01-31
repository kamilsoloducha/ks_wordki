import { call, takeLatest, select } from "@redux-saga/core/effects";
import { DailyActionEnum, TickCard } from "../actions";
import * as api from "pages/lesson/services/repeatsApi";
import { selectUserId } from "store/user/selectors";
import { ApiResponse } from "common/models/response";

function* tickCard({ sideId }: TickCard) {
  const userId: string = yield select(selectUserId);
  const response: ApiResponse<any> = yield call(
    async () => await api.tickCard({ userId, sideId })
  );
  if (!response.isCorrect) {
    console.error("Error occured");
  }
}

export default function* tickCardEffect() {
  yield takeLatest(DailyActionEnum.TICK_CARD, tickCard);
}
