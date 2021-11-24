import { call, put, takeLatest } from "@redux-saga/core/effects";
import { DailyActionEnum, getCardsCountSuccess } from "../actions";
import * as api from "pages/lesson/services/repeatsApi";

function* getCardsCount() {
  const { data }: { data: number; error: any } = yield call(() =>
    api.repeatsCount()
  );
  yield put(getCardsCountSuccess(data));
}

export default function* getCardsCountEffect() {
  yield takeLatest(DailyActionEnum.GET_CARDS_COUNT, getCardsCount);
}
