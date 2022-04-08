import { put } from "@redux-saga/core/effects";
import { LessonSettings } from "pages/lessonSettings/models/lessonSettings";
import { select, take } from "redux-saga/effects";
import { DailyActionEnum, getCardsCount } from "../actions";
import { selectSettings } from "../selectors";
import * as lessonMode from "pages/lessonSettings/models/lesson-mode";
import { SagaIterator } from "redux-saga";

export function* setSettingLanguageEffect(): SagaIterator {
  yield take(DailyActionEnum.SET_SETTING_LANGUAGE);
  const settings: LessonSettings = yield select(selectSettings);
  if (settings.mode === lessonMode.LessonMode.New) {
    return;
  }
  yield put(getCardsCount());
}
