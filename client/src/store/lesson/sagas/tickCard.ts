import { call, select } from "@redux-saga/core/effects";
import * as api from "api";
import { selectUserId } from "store/user/selectors";
import { ApiResponse } from "common/models/response";
import { selectCurrectRepeat } from "../selectors";
import { Repeat } from "pages/lesson/models/repeat";
import { SagaIterator } from "redux-saga";
import { take } from "redux-saga/effects";

export function* tickCardEffect(): SagaIterator {
  yield take("lesson/tickCard");

  const userId: string = yield select(selectUserId);
  const repeat: Repeat = yield select(selectCurrectRepeat);
  const request = { userId, sideId: repeat.sideId } as api.TickCardRequest;
  const response: ApiResponse<any> = yield call(api.tickCard, request);
  if (!response.isCorrect) {
    console.error("Error occured");
  }
}
