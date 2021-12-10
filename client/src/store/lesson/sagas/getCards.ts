import { call, put, select, takeLatest } from "@redux-saga/core/effects";
import { GetRepeatsResponse } from "pages/lesson/models/getRepeatsResponse";
import { DailyActionEnum, GetCards, getCardsSuccess } from "../actions";
import * as api from "pages/lesson/services/repeatsApi";
import { selectUserId } from "store/user/selectors";
import GetRepeatsRequest from "pages/lesson/models/getRepeatsRequest";
import { selectLessonType } from "../selectors";
import StartLessonRequest from "pages/lesson/models/startLessonRequest";

function* getCards(action: GetCards) {
  const getRepeatsRequest = {
    count: action.count,
    questionLanguage: action.questionLanguage,
  } as GetRepeatsRequest;

  const { data }: { data: GetRepeatsResponse; error: any } = yield call(() =>
    api.repeats(getRepeatsRequest)
  );
  const userId: string = yield select(selectUserId);
  const lessonType: number = yield select(selectLessonType);
  const startLessonRequest = { userId, lessonType } as StartLessonRequest;

  yield call(() => api.startLesson(startLessonRequest));
  const repeats = shuffle(data.repeats);
  yield put(getCardsSuccess(repeats));
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
