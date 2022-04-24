import "test/matcher/toDeepEqual";
import * as cards from "api/services/cards";
import { SagaIterator } from "redux-saga";
import { call, put, select, take } from "redux-saga/effects";
import { selectUserId } from "store/user/selectors";
import { deleteCardEffect } from "../deleteCardEffect";
import { deleteCard, search } from "store/cardsSearch/reducer";

describe("deleteCardEffect", () => {
  let saga: SagaIterator;
  let mock: any;

  beforeEach(() => {
    mock = jest.spyOn(cards, "deleteCard");
    saga = deleteCardEffect();
  });

  afterEach(() => {
    jest.restoreAllMocks();
  });

  it("should go through", () => {
    const action = deleteCard({ cardId: "1", groupId: "2" });
    const userId = "userId";
    const response = "response";
    expect(saga.next().value).toStrictEqual(take("cardsSearch/deleteCard"));
    expect(saga.next(action).value).toStrictEqual(select(selectUserId));
    expect(saga.next(userId).value).toStrictEqual(call(mock, userId, "2", "1"));
    expect(saga.next(response).value).toDeepEqual(put(search()));
  });
});
