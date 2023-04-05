import * as api from "api";
import { call, put, select } from "@redux-saga/core/effects";
import { selectUserId } from "store/user/selectors";
import { selectLessonType, selectSettings } from "../selectors";
import { LessonSettings } from "pages/lessonSettings/models/lessonSettings";
import history from "common/services/history";
import { LessonMode } from "pages/lessonSettings/models/lesson-mode";
import { SagaIterator } from "redux-saga";
import { take } from "redux-saga/effects";
import { getCardsSuccess } from "../reducer";
import { Repeat } from "pages/lesson/models/repeat";

export function* getCardsEffect(): SagaIterator {
  while (true) {
    yield take("lesson/getCards");
    const userId: string = yield select(selectUserId);
    const settings: LessonSettings = yield select(selectSettings);

    const getRepeatsRequest = prepareRequest(settings);

    const repeats: Repeat[] = yield call(api.repeats, getRepeatsRequest);

    if (repeats.length === 0) {
      yield call(forwardTo, "/dashboard");
      continue;
    }

    const lessonType: number = yield select(selectLessonType);
    const startLessonRequest = { userId, lessonType } as api.StartLessonRequest;

    yield call(api.startLesson, startLessonRequest);
    yield put(getCardsSuccess({ repeats }));
    yield call(forwardTo, "/lesson");
  }
}

function prepareRequest(settings: LessonSettings): api.RepeatsQuery {
  const request: api.RepeatsQuery = {
    count: settings.count,
    languages: settings.languages,
    groupId: settings.mode === LessonMode.Repetition ? null : settings.selectedGroupId,
    lessonIncluded: settings.mode === LessonMode.Repetition,
  };
  return request;
}

function forwardTo(location: any) {
  history.push(location);
}
