import "test/matcher/toDeepEqual";
import * as cards from "api/services/cards";
import * as groups from "api/services/groups";
import * as api from "api";
import { SagaIterator } from "redux-saga";
import { call, put, select, take, takeEvery } from "redux-saga/effects";
import { selectUserId } from "store/user/selectors";
import { getCardsEffect, getCardsWorker } from "../getCards";
import { applyFilters, getCards, getCardsSuccess } from "store/cards/reducer";

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
    expect(saga.next().value).toStrictEqual(takeEvery("cards/getCards", getCardsWorker));
  });

  // it("should go through", () => {
  //   const groupId = "groupId";
  //   const action = getCards({ groupId: groupId });
  //   const userId = "userId";
  //   const cardsSummaryResponse: api.CardsSummaryResponse = {
  //     cards: [{} as any],
  //   };
  //   const groupDetailsResponse: api.GroupDetailsResponse = {
  //     id: "groupId",
  //     name: "name",
  //     front: 1,
  //     back: 2,
  //   };
  //   expect(saga.next().value).toStrictEqual(take("cards/getCards"));
  //   expect(saga.next(action).value).toStrictEqual(select(selectUserId));
  //   expect(saga.next(userId).value).toStrictEqual(call(cardsSummaryMock, userId, groupId));
  //   expect(saga.next(cardsSummaryResponse).value).toStrictEqual(call(groupDetailsMock, groupId));
  //   expect(saga.next(groupDetailsResponse).value).toDeepEqual(
  //     put(
  //       getCardsSuccess({
  //         id: groupDetailsResponse.id,
  //         name: groupDetailsResponse.name,
  //         language1: groupDetailsResponse.front,
  //         language2: groupDetailsResponse.back,
  //         cards: cardsSummaryResponse.cards,
  //       })
  //     )
  //   );
  //   expect(saga.next().value).toDeepEqual(put(applyFilters()));
  // });
});
