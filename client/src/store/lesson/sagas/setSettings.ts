import { put, takeLatest } from "@redux-saga/core/effects";
import { LessonSettings } from "pages/lessonSettings/models/lessonSettings";
import { select } from "redux-saga/effects";
import { DailyActionEnum, getCardsCount } from "../actions";
import { selectSettings } from "../selectors";

import * as lessonMode from "pages/lessonSettings/models/lesson-mode";

function* setSettingLanguage() {
  const settings: LessonSettings = yield select(selectSettings);
  if (settings.mode === lessonMode.New) {
    return;
  }
  yield put(getCardsCount());
}

export default function* setSettingLanguageEffect() {
  yield takeLatest(DailyActionEnum.SET_SETTING_LANGUAGE, setSettingLanguage);
}
