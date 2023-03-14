import { call, put, takeEvery } from "redux-saga/effects";
import * as api from "api";
import { getLanguagesSuccess } from "../reducer";
import { Language } from "pages/lessonSettings/models/languages";

export function* getLanguagesEffect() {
  yield takeEvery("lesson/getLanguages", getLanguages);
}

export function* getLanguages() {
  var languages: Language[] = yield call(api.getLanguages);

  yield put(getLanguagesSuccess({ languages: languages }));
}
