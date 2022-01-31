import { put, takeLatest } from "@redux-saga/core/effects";
import { DailyActionEnum, getCardsCount } from "../actions";

function* setSettingLanguage() {
  yield put(getCardsCount());
}

export default function* setSettingLanguageEffect() {
  yield takeLatest(DailyActionEnum.SET_SETTING_LANGUAGE, setSettingLanguage);
}
