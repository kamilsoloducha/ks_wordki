import "test/matcher/toDeepEqual";
import * as cards from "api/services/cards";
import { SagaIterator } from "redux-saga";
import { call, put, select, take } from "redux-saga/effects";
import { selectUserId } from "store/user/selectors";
import { getOverview, getOverviewSuccess, searchSuccess } from "store/cardsSearch/reducer";
import { CardsOverview, CardSummary, Filter } from "pages/cardsSearch/models";
import { searchEffect } from "../searchEffect";
import { selectFilter } from "store/cardsSearch/selectors";
import { CardsSearchQuery } from "api";

describe("searchEffect", () => {
  let saga: SagaIterator;
  let mockSearchCards: any;
  let mockSearchCardsCount: any;

  beforeEach(() => {
    mockSearchCards = jest.spyOn(cards, "searchCards");
    mockSearchCardsCount = jest.spyOn(cards, "searchCardsCount");
    saga = searchEffect();
  });

  afterEach(() => {
    jest.restoreAllMocks();
  });

  it("should go through", () => {
    const action = getOverview();
    const userId = "userId";
    const filter: Filter = {
      searchingTerm: "test",
      tickedOnly: true,
      lessonIncluded: true,
      pageNumber: 1,
      pageSize: 2,
    };

    const searchRequest: CardsSearchQuery = {
      ownerId: userId,
      searchingTerm: filter.searchingTerm,
      pageNumber: filter.pageNumber,
      pageSize: filter.pageSize,
      onlyTicked: filter.tickedOnly ?? false,
      searchingDrawers: [],
      lessonIncluded: filter.lessonIncluded,
    };

    const searchCardsResponse: CardSummary[] = [{} as any, {} as any];
    const searchCardsCountResponse = 2;

    expect(saga.next().value).toStrictEqual(take("cardsSearch/search"));
    expect(saga.next(action).value).toStrictEqual(select(selectUserId));
    expect(saga.next(userId).value).toStrictEqual(select(selectFilter));
    expect(saga.next(filter).value).toStrictEqual(call(mockSearchCards, searchRequest));
    expect(saga.next(searchCardsResponse).value).toStrictEqual(
      call(mockSearchCardsCount, searchRequest)
    );
    expect(saga.next(searchCardsCountResponse).value).toDeepEqual(
      put(searchSuccess({ cards: searchCardsResponse, cardsCount: searchCardsCountResponse }))
    );
  });
});
