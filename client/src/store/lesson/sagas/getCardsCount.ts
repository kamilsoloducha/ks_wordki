import { call, put, takeLatest, select } from "@redux-saga/core/effects";
import { DailyActionEnum, getCardsCountSuccess } from "../actions";
import * as api from "api";
import { selectUserId } from "store/user/selectors";
import { ApiResponse } from "common/models/response";
import { LessonSettings } from "pages/lessonSettings/models/lessonSettings";
import { selectSettings } from "../selectors";

function* getCardsCount() {
  const userId: string = yield select(selectUserId);
  const settings: LessonSettings = yield select(selectSettings);
  const apiResposne: ApiResponse<number> = yield call(
    async () =>
      await api.repeatsCount({
        questionLanguage: settings.languages ?? [],
        userId: userId,
      })
  );
  yield put(getCardsCountSuccess(apiResposne.response));
}

export function* getCardsCountEffect() {
  yield takeLatest(DailyActionEnum.GET_CARDS_COUNT, getCardsCount);
}
