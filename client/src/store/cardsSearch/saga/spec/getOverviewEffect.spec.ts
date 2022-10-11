import "test/matcher/toDeepEqual";
import * as cards from "api/services/cards";
import { SagaIterator } from "redux-saga";
import { call, put, select, take, takeEvery } from "redux-saga/effects";
import { selectUserId } from "store/user/selectors";
import { getOverview, getOverviewSuccess } from "store/cardsSearch/reducer";
import { getOverviewEffect, getOverviewWorker } from "../getOverviewEffect";
import { CardsOverview } from "pages/cardsSearch/models";

describe("getOverviewEffect", () => {
  let saga: SagaIterator;
  let mock: any;

  beforeEach(() => {
    mock = jest.spyOn(cards, "cardsOverview");
    saga = getOverviewEffect();
  });

  afterEach(() => {
    jest.restoreAllMocks();
  });

  it("should go through", () => {
    expect(saga.next().value).toStrictEqual(takeEvery("cardsSearch/getOverview", getOverviewWorker));
  });
});
