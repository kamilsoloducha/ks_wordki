import "test/matcher/toDeepEqual";
import * as cards from "api/services/cards";
import * as groups from "api/services/groups";
import * as api from "api";
import * as actions from "../../actions";
import { SagaIterator } from "redux-saga";
import { call, put, select, take } from "redux-saga/effects";
import { selectUserId } from "store/user/selectors";
import { selectGroupId, selectSelectedCard } from "store/cards/selectors";
import { CardSummary } from "pages/cards/models";
import { getCardsEffect } from "../getCards";

describe("getCardsEffect", () => {
  let saga: SagaIterator;
  let cardsSummaryMock: any;
  let groupDetailsMock: any;

  beforeEach(() => {
    cardsSummaryMock = jest.spyOn(cards, "cardsSummary");
    groupDetailsMock = jest.spyOn(groups, "groupDetails");
    saga = getCardsEffect();
  });

  afterEach(() => {
    jest.restoreAllMocks();
  });

  it("should go through", () => {
    const groupId = "groupId";
    const action: actions.GetCards = actions.getCards(groupId);
    const userId = "userId";
    const cardsSummaryResponse: api.CardsSummaryResponse = {
      cards: [{} as any],
    };
    const groupDetailsResponse: api.GroupDetailsResponse = {
      id: "groupId",
      name: "name",
      front: 1,
      back: 2,
    };
    expect(saga.next().value).toStrictEqual(take(actions.CardsActionEnum.GET_CARDS));
    expect(saga.next(action).value).toStrictEqual(select(selectUserId));
    expect(saga.next(userId).value).toStrictEqual(call(cardsSummaryMock, userId, groupId));
    expect(saga.next(cardsSummaryResponse).value).toStrictEqual(call(groupDetailsMock, groupId));
    expect(saga.next(groupDetailsResponse).value).toDeepEqual(
      put(
        actions.getCardsSuccess(
          groupDetailsResponse.id,
          groupDetailsResponse.name,
          groupDetailsResponse.front,
          groupDetailsResponse.back,
          cardsSummaryResponse.cards
        )
      )
    );
    expect(saga.next().value).toDeepEqual(put(actions.applyFilters()));
  });
});
