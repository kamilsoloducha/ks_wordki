import * as api from "api";
import { call, put, select } from "@redux-saga/core/effects";
import { selectUserId } from "store/user/selectors";
import { selectLessonType, selectSettings } from "../selectors";
import { ApiResponse } from "common/models/response";
import { LessonSettings } from "pages/lessonSettings/models/lessonSettings";
import history from "../../../common/services/history";
import { LessonMode } from "pages/lessonSettings/models/lesson-mode";
import { SagaIterator } from "redux-saga";
import { take } from "redux-saga/effects";
import { getCardsSuccess } from "../reducer";

export function* getCardsEffect(): SagaIterator {
  yield take("lesson/getCards");
  const userId: string = yield select(selectUserId);
  const settings: LessonSettings = yield select(selectSettings);

  const getRepeatsRequest = prepareRequest(settings, userId);

  const apiResponse: ApiResponse<api.GetRepeatsResponse> = yield call(
    async () => await api.repeats(getRepeatsRequest)
  );
  const lessonType: number = yield select(selectLessonType);
  const startLessonRequest = { userId, lessonType } as api.StartLessonRequest;

  yield call(async () => await api.startLesson(startLessonRequest));
  yield put(getCardsSuccess({ repeats: apiResponse.response.repeats }));
  yield call(forwardTo, "/lesson");
}

function prepareRequest(settings: LessonSettings, userId: string): api.RepeatsQuery {
  const request: api.RepeatsQuery = {
    count: settings.count,
    questionLanguage: settings.languages,
    ownerId: userId,
    groupId: settings.mode === LessonMode.Repetition ? null : settings.selectedGroupId,
    lessonIncluded: settings.mode === LessonMode.Repetition,
  };
  return request;
}

function forwardTo(location: any) {
  console.log(location);
  history.go(location);
}
