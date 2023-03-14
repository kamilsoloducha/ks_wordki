import { call, select } from "@redux-saga/core/effects";
import * as api from "api";
import { ApiResponse } from "common/models/response";
import { selectCurrectRepeat } from "../selectors";
import { Repeat } from "pages/lesson/models/repeat";
import { SagaIterator } from "redux-saga";
import { take } from "redux-saga/effects";

export function* tickCardEffect(): SagaIterator {
  while (true) {
    yield take("lesson/tickCard");

    const repeat: Repeat = yield select(selectCurrectRepeat);
    const request = { cardId: repeat.cardId } as api.TickCardRequest;
    const response: ApiResponse<any> = yield call(api.tickCard, request);
    if (!response.isCorrect) {
      console.error("Error occured");
    }
  }
}
