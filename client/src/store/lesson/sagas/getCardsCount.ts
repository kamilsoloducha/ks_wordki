import { call, put, takeLatest } from "@redux-saga/core/effects";
import {
  DailyActionEnum,
  GetCardsCount,
  getCardsCountSuccess,
} from "../actions";
import * as api from "pages/lesson/services/repeatsApi";

function* getCardsCount({ questionLanguage }: GetCardsCount) {
  const { data }: { data: number; error: any } = yield call(() =>
    api.repeatsCount({ questionLanguage: questionLanguage })
  );
  yield put(getCardsCountSuccess(data));
}

export default function* getCardsCountEffect() {
  yield takeLatest(DailyActionEnum.GET_CARDS_COUNT, getCardsCount);
}
