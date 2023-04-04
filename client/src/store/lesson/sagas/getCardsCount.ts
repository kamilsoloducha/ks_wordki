import { call, put, select, take } from "@redux-saga/core/effects";
import * as api from "api";
import { ApiResponse } from "common/models/response";
import { LessonSettings } from "pages/lessonSettings/models/lessonSettings";
import { selectSettings } from "../selectors";
import { SagaIterator } from "redux-saga";
import { getCardsCountSuccess } from "../reducer";

export function* getCardsCountEffect(): SagaIterator {
  while (true) {
    yield take("lesson/getCardsCount");
    const settings: LessonSettings = yield select(selectSettings);
    const apiResposne: ApiResponse<number> = yield call(api.repeatsCount, {
      languages: settings.languages ?? [],
    });
    yield put(getCardsCountSuccess({ count: apiResposne.response }));
  }
}
