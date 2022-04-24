import "test/matcher/toDeepEqual";
import * as cards from "api/services/cards";
import { SagaIterator } from "redux-saga";
import { call, put, select, take } from "redux-saga/effects";
import { selectUserId } from "store/user/selectors";
import { search, updateCard } from "store/cardsSearch/reducer";
import { updateCardEffect } from "../updateCardEffect";
import { UpdateCardRequest } from "api";

describe("updateCardEffect", () => {
  let saga: SagaIterator;
  let mock: any;

  beforeEach(() => {
    mock = jest.spyOn(cards, "updateCard2");
    saga = updateCardEffect();
  });

  afterEach(() => {
    jest.restoreAllMocks();
  });

  it("should go through", () => {
    const action = updateCard({
      card: {
        id: "id",
        groupId: "groupId",
        groupName: "groupName",
        front: { value: "frontV", example: "frontE", isUsed: true, isTicked: true } as any,
        back: { value: "backV", example: "backE", isUsed: true, isTicked: true } as any,
      },
    });
    const userId = "userId";
    const request: UpdateCardRequest = {
      userId: userId,
      groupId: action.payload.card.groupId,
      cardId: action.payload.card.id,
      front: {
        value: action.payload.card.front.value,
        example: action.payload.card.front.example,
        isUsed: action.payload.card.front.isUsed,
        isTicked: action.payload.card.front.isTicked,
      },
      back: {
        value: action.payload.card.back.value,
        example: action.payload.card.back.example,
        isUsed: action.payload.card.back.isUsed,
        isTicked: action.payload.card.back.isTicked,
      },
    };
    expect(saga.next().value).toStrictEqual(take("cardsSerach/updateCard"));
    expect(saga.next(action).value).toStrictEqual(select(selectUserId));
    expect(saga.next(userId).value).toStrictEqual(call(mock, request));
    expect(saga.next().value).toDeepEqual(put(search()));
  });
});
