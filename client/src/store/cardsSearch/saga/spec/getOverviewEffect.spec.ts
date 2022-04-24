import "test/matcher/toDeepEqual";
import * as cards from "api/services/cards";
import { SagaIterator } from "redux-saga";
import { call, put, select, take } from "redux-saga/effects";
import { selectUserId } from "store/user/selectors";
import { getOverview, getOverviewSuccess } from "store/cardsSearch/reducer";
import { getOverviewEffect } from "../getOverviewEffect";
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
    const action = getOverview();
    const userId = "userId";
    const response: CardsOverview = { all: 1, drawers: [1, 2, 3], lessonIncluded: 2, ticked: 3 };
    expect(saga.next().value).toStrictEqual(take("cardsSearch/getOverview"));
    expect(saga.next(action).value).toStrictEqual(select(selectUserId));
    expect(saga.next(userId).value).toStrictEqual(call(mock, userId));
    expect(saga.next(response).value).toDeepEqual(put(getOverviewSuccess({ overview: response })));
  });
});
