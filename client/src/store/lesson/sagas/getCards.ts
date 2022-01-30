import { call, put, select, takeLatest } from "@redux-saga/core/effects";
import { GetRepeatsResponse } from "pages/lesson/models/getRepeatsResponse";
import { DailyActionEnum, GetCards, getCardsSuccess } from "../actions";
import * as api from "pages/lesson/services/repeatsApi";
import { selectUserId } from "store/user/selectors";
import GetRepeatsRequest from "pages/lesson/models/getRepeatsRequest";
import { selectLessonType } from "../selectors";
import StartLessonRequest from "pages/lesson/models/startLessonRequest";

function* getCards(action: GetCards) {
  const userId: string = yield select(selectUserId);
  const getRepeatsRequest = {
    count: action.count,
    questionLanguage: action.questionLanguage,
    ownerId: userId,
  } as GetRepeatsRequest;

  const { data }: { data: GetRepeatsResponse; error: any } = yield call(() =>
    api.repeats(getRepeatsRequest)
  );
  const lessonType: number = yield select(selectLessonType);
  const startLessonRequest = { userId, lessonType } as StartLessonRequest;

  yield call(() => api.startLesson(startLessonRequest));
  yield put(getCardsSuccess(data.repeats));
}

export default function* getCardsEffect() {
  yield takeLatest(DailyActionEnum.GET_CARDS, getCards);
}

function shuffle<T>(array: T[]): T[] {
  let currentIndex = array.length,
    randomIndex;

  while (currentIndex !== 0) {
    randomIndex = Math.floor(Math.random() * currentIndex);
    currentIndex--;

    [array[currentIndex], array[randomIndex]] = [
      array[randomIndex],
      array[currentIndex],
    ];
  }

  return array;
}
