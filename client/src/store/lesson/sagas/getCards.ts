import { call, put, select, takeLatest } from "@redux-saga/core/effects";
import { GetRepeatsResponse } from "pages/lesson/models/getRepeatsResponse";
import { DailyActionEnum, GetCards, getCardsSuccess } from "../actions";
import * as api from "pages/lesson/services/repeatsApi";
import { selectUserId } from "store/user/selectors";

function* getCards(action: GetCards) {
  const userId: string = yield select(selectUserId);
  const { data }: { data: GetRepeatsResponse; error: any } = yield call(() =>
    api.repeats(action.count)
  );
  yield call(() => api.startLesson(userId));
  yield put(getCardsSuccess(data.repeats));
}

export default function* getCardsEffect() {
  yield takeLatest(DailyActionEnum.GET_CARDS, getCards);
}
