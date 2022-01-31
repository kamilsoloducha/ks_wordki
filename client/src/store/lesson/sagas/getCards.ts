import { call, put, select, takeLatest } from "@redux-saga/core/effects";
import { DailyActionEnum, getCardsSuccess } from "../actions";
import * as api from "pages/lesson/services/repeatsApi";
import { selectUserId } from "store/user/selectors";
import { selectLessonType, selectSettings } from "../selectors";
import {
  GetRepeatsRequest,
  GetRepeatsResponse,
  StartLessonRequest,
} from "pages/lesson/requests";
import { ApiResponse } from "common/models/response";
import { LessonSettings } from "pages/lessonSettings/models/lessonSettings";

function* getCards() {
  const userId: string = yield select(selectUserId);
  const settings: LessonSettings = yield select(selectSettings);
  const getRepeatsRequest = {
    count: settings.count,
    questionLanguage: settings.language,
    ownerId: userId,
  } as GetRepeatsRequest;

  const apiResponse: ApiResponse<GetRepeatsResponse> = yield call(
    async () => await api.repeats(getRepeatsRequest)
  );
  const lessonType: number = yield select(selectLessonType);
  const startLessonRequest = { userId, lessonType } as StartLessonRequest;

  yield call(async () => await api.startLesson(startLessonRequest));
  yield put(getCardsSuccess(apiResponse.response.repeats));
}

export default function* getCardsEffect() {
  yield takeLatest(DailyActionEnum.GET_CARDS, getCards);
}
