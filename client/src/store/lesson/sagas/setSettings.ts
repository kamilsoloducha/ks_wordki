import { put } from "@redux-saga/core/effects";
import { LessonSettings } from "pages/lessonSettings/models/lessonSettings";
import { select, take } from "redux-saga/effects";
import { selectSettings } from "../selectors";
import * as lessonMode from "pages/lessonSettings/models/lesson-mode";
import { SagaIterator } from "redux-saga";
import { getCardsCount } from "../reducer";

export function* setSettingLanguageEffect(): SagaIterator {
  yield take("lesson/setSettingLanguage");
  const settings: LessonSettings = yield select(selectSettings);
  if (settings.mode === lessonMode.LessonMode.New) {
    return;
  }
  yield put(getCardsCount());
}
