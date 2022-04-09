import "test/matcher/toDeepEqual";
import * as cards from "api/services/cards";
import * as api from "api";
import * as actions from "../../actions";
import { SagaIterator } from "redux-saga";
import { call, put, select, take } from "redux-saga/effects";
import { selectUserId } from "store/user/selectors";
import { selectGroupId } from "store/cards/selectors";
import { CardSummary } from "pages/cards/models";
import { updateCardEffect } from "../updateCard";

describe("updateCardEffect", () => {
  let saga: SagaIterator;
  let mock: any;

  beforeEach(() => {
    mock = jest.spyOn(cards, "updateCard");
    saga = updateCardEffect();
  });

  afterEach(() => {
    jest.restoreAllMocks();
  });

  it("should go through", () => {
    const action: actions.UpdateCard = actions.updateCard({} as CardSummary);
    const userId = "userId";
    const groupId = "groupId";
    const response = "response";
    expect(saga.next().value).toStrictEqual(take(actions.CardsActionEnum.UPDATE_CARD));
    expect(saga.next(action).value).toStrictEqual(select(selectUserId));
    expect(saga.next(userId).value).toStrictEqual(select(selectGroupId));
    expect(saga.next(groupId).value).toStrictEqual(call(mock, userId, groupId, {} as CardSummary));
    expect(saga.next(response).value).toDeepEqual(
      put(actions.updateCardSuccess({} as CardSummary))
    );
  });
});
