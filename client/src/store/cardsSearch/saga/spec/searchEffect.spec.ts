import "test/matcher/toDeepEqual";
import * as cards from "api/services/cards";
import { SagaIterator } from "redux-saga";
import { all, call, put, select } from "redux-saga/effects";
import { selectUserId } from "store/user/selectors";
import { getOverview, searchSuccess } from "store/cardsSearch/reducer";
import { CardSummary, Filter } from "pages/cardsSearch/models";
import { searchWorker } from "../searchEffect";
import { selectFilter } from "store/cardsSearch/selectors";
import { CardsSearchQuery } from "api";

describe("searchEffect", () => {
  let saga: SagaIterator;
  let mockSearchCards: any;
  let mockSearchCardsCount: any;

  beforeEach(() => {
    mockSearchCards = jest.spyOn(cards, "searchCards");
    mockSearchCardsCount = jest.spyOn(cards, "searchCardsCount");
    saga = searchWorker();
  });

  afterEach(() => {
    jest.restoreAllMocks();
  });

  it("should go through", () => {
    const userId = "userId";
    const filter: Filter = {
      searchingTerm: "test",
      tickedOnly: true,
      lessonIncluded: true,
      pageNumber: 1,
      pageSize: 2,
    };

    const searchRequest: CardsSearchQuery = {
      searchingTerm: filter.searchingTerm,
      pageNumber: filter.pageNumber,
      pageSize: filter.pageSize,
      isTicked: filter.tickedOnly,
      searchingDrawers: [],
      lessonIncluded: filter.lessonIncluded,
    };

    const searchCardsResponse: CardSummary[] = [{} as any, {} as any];
    const searchCardsCountResponse = 2;

    expect(saga.next(userId).value).toStrictEqual(select(selectFilter));
    expect(saga.next(filter).value).toStrictEqual(
      all([call(mockSearchCards, searchRequest), call(mockSearchCardsCount, searchRequest)])
    );
    expect(saga.next([searchCardsResponse, searchCardsCountResponse]).value).toDeepEqual(
      put(searchSuccess({ cards: searchCardsResponse, cardsCount: searchCardsCountResponse }))
    );
  });
});
