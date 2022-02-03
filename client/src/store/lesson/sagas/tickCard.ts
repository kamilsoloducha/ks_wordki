import { call, takeLatest, select } from "@redux-saga/core/effects";
import { DailyActionEnum } from "../actions";
import * as api from "pages/lesson/services/repeatsApi";
import { selectUserId } from "store/user/selectors";
import { ApiResponse } from "common/models/response";
import { selectCurrectRepeat } from "../selectors";
import { Repeat } from "pages/lesson/models/repeat";
import { TickCardRequest } from "pages/lesson/requests";

function* tickCard() {
  const userId: string = yield select(selectUserId);
  const repeat: Repeat = yield select(selectCurrectRepeat);
  const request = { userId, sideId: repeat.sideId } as TickCardRequest;
  const response: ApiResponse<any> = yield call(
    async () => await api.tickCard(request)
  );
  if (!response.isCorrect) {
    console.error("Error occured");
  }
}

export default function* tickCardEffect() {
  yield takeLatest(DailyActionEnum.TICK_CARD, tickCard);
}
