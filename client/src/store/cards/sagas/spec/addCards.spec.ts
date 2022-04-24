import "test/matcher/toDeepEqual";
import * as cards from "api/services/cards";
import { addCardEffect } from "../addCard";
import { SagaIterator } from "redux-saga";
import { call, put, select, take } from "redux-saga/effects";
import { selectUserId } from "store/user/selectors";
import { selectGroupId } from "store/cards/selectors";
import { CardSummary } from "pages/cards/models";
import { addCard, selectCard } from "store/cards/reducer";

describe("addCardEffect", () => {
  let saga: SagaIterator;
  let mock: any;

  beforeEach(() => {
    mock = jest.spyOn(cards, "addCard");
    saga = addCardEffect();
  });

  afterEach(() => {
    jest.restoreAllMocks();
  });

  it("should go through", () => {
    const action = addCard({ card: {} as CardSummary });
    const userId = "userId";
    const groupId = "groupId";
    const response = "response";
    expect(saga.next().value).toStrictEqual(take("cards/addCard"));
    expect(saga.next(action).value).toStrictEqual(select(selectUserId));
    expect(saga.next(userId).value).toStrictEqual(select(selectGroupId));
    expect(saga.next(groupId).value).toStrictEqual(call(mock, userId, groupId, {} as CardSummary));
    expect(saga.next(response).value).toDeepEqual(put(selectCard({ item: {} as CardSummary })));
  });
});
