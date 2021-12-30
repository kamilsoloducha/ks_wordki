import { call, takeLatest, select } from "@redux-saga/core/effects";
import { DailyActionEnum, TickCard } from "../actions";
import * as api from "pages/lesson/services/repeatsApi";
import { selectUserId } from "store/user/selectors";

function* tickCard({ cardId, groupId }: TickCard) {
  const userId: string = yield select(selectUserId);
  const { data }: { data: number; error: any } = yield call(() =>
    api.tickCard({ userId, cardId, groupId })
  );
}

export default function* tickCardEffect() {
  yield takeLatest(DailyActionEnum.TICK_CARD, tickCard);
}
