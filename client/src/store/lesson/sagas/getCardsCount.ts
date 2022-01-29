import { call, put, takeLatest, select } from "@redux-saga/core/effects";
import {
  DailyActionEnum,
  GetCardsCount,
  getCardsCountSuccess,
} from "../actions";
import * as api from "pages/lesson/services/repeatsApi";
import { selectUserId } from "store/user/selectors";

function* getCardsCount({ questionLanguage }: GetCardsCount) {
  const userId: string = yield select(selectUserId);
  const { data }: { data: number; error: any } = yield call(() =>
    api.repeatsCount({ questionLanguage: questionLanguage, userId: userId })
  );
  yield put(getCardsCountSuccess(data));
}

export default function* getCardsCountEffect() {
  yield takeLatest(DailyActionEnum.GET_CARDS_COUNT, getCardsCount);
}
