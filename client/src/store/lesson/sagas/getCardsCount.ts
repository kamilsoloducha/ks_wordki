import { call, put, select, take } from "@redux-saga/core/effects";
import * as api from "api";
import { selectUserId } from "store/user/selectors";
import { ApiResponse } from "common/models/response";
import { LessonSettings } from "pages/lessonSettings/models/lessonSettings";
import { selectSettings } from "../selectors";
import { SagaIterator } from "redux-saga";
import { getCardsCountSuccess } from "../reducer";

export function* getCardsCountEffect(): SagaIterator {
  yield take("lesson/getCardsCount");
  const userId: string = yield select(selectUserId);
  const settings: LessonSettings = yield select(selectSettings);
  const apiResposne: ApiResponse<number> = yield call(
    async () =>
      await api.repeatsCount({
        questionLanguage: settings.languages ?? [],
        userId: userId,
      })
  );
  yield put(getCardsCountSuccess({ count: apiResposne.response }));
}
