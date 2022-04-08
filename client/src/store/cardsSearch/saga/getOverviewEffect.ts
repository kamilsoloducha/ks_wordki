import { call, put, select, takeLatest } from "@redux-saga/core/effects";
import * as actions from "../actions";
import * as api from "api";
import { selectUserId } from "store/user/selectors";
import { CardsOverview } from "pages/cardsSearch/models/cardsOverview";
import { take } from "redux-saga/effects";

export function* getOverviewEffect() {
  yield take(actions.CardsSearchActionEnum.GET_OVERVIEW);
  const userId: string = yield select(selectUserId);

  const overview: CardsOverview = yield call(api.cardsOverview, userId);

  yield put(actions.getOverviewSuccess(overview));
}
