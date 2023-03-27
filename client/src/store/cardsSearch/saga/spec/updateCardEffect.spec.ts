import "test/matcher/toDeepEqual";
import * as cards from "api/services/cards";
import { SagaIterator } from "redux-saga";
import { call, put } from "redux-saga/effects";
import { search, updateCard } from "store/cardsSearch/reducer";
import { updateCardWorker } from "../updateCardEffect";
import { UpdateCardRequest } from "api";

describe("updateCardEffect", () => {
  let saga: SagaIterator;
  let mock: any;

  beforeEach(() => {
    mock = jest.spyOn(cards, "updateCard2");
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
      comment: "",
    };

    saga = updateCardWorker(action);

    expect(saga.next(userId).value).toStrictEqual(call(mock, request));
    expect(saga.next().value).toDeepEqual(put(search()));
  });
});
