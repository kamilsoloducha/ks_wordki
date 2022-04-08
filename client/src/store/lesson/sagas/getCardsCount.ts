import { call, put, takeLatest, select, take } from "@redux-saga/core/effects";
import { DailyActionEnum, getCardsCountSuccess } from "../actions";
import * as api from "api";
import { selectUserId } from "store/user/selectors";
import { ApiResponse } from "common/models/response";
import { LessonSettings } from "pages/lessonSettings/models/lessonSettings";
import { selectSettings } from "../selectors";
import { SagaIterator } from "redux-saga";

export function* getCardsCountEffect(): SagaIterator {
  yield take(DailyActionEnum.GET_CARDS_COUNT);
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
