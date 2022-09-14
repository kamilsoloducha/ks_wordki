import { call, put, select } from "@redux-saga/core/effects";
import * as api from "api";
import { selectUserId } from "store/user/selectors";
import { CardsOverview } from "pages/cardsSearch/models/cardsOverview";
import { take } from "redux-saga/effects";
import { SagaIterator } from "redux-saga";
import { getOverviewSuccess } from "../reducer";

export function* getOverviewEffect(): SagaIterator {
  while (true) {
    yield take("cardsSearch/getOverview");
    const userId: string = yield select(selectUserId);

    const overview: CardsOverview = yield call(api.cardsOverview, userId);

    yield put(getOverviewSuccess({ overview }));
  }
}