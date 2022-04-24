import "test/matcher/toDeepEqual";
import * as cards from "api/services/cards";
import { SagaIterator } from "redux-saga";
import { call, put, select, take } from "redux-saga/effects";
import { selectUserId } from "store/user/selectors";
import { deleteCardEffect } from "../deleteCardEffect";
import { deleteCard, search } from "store/cardsSearch/reducer";
import { setPaginationEffect } from "../setFilteringEffects";

describe("setPaginationEffect", () => {
  let saga: SagaIterator;

  beforeEach(() => {
    saga = setPaginationEffect();
  });

  afterEach(() => {
    jest.restoreAllMocks();
  });

  it("should go through", () => {
    expect(saga.next().value).toStrictEqual(
      take([
        "cardsSerach/filterReset",
        "cardsSerach/filterSetPagination",
        "cardsSerach/filterSetLessonIncluded",
        "cardsSerach/filterSetTickedOnly",
      ])
    );
    expect(saga.next().value).toDeepEqual(put(search()));
  });
});
