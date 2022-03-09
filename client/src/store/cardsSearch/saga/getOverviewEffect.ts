import { call, put, select, takeLatest } from "@redux-saga/core/effects";
import * as actions from "../actions";
import * as api from "pages/cards/services/groupDetailsApi";
import { selectUserId } from "store/user/selectors";
import { CardsOverview } from "pages/cards/models/cardsOverview";

function* getOverview() {
  const userId: string = yield select(selectUserId);

  const overview: CardsOverview = yield call(api.cardsOverview, userId);

  yield put(actions.getOverviewSuccess(overview));
}

export function* getOverviewEffect() {
  yield takeLatest(actions.CardsSearchActionEnum.GET_OVERVIEW, getOverview);
}
