import "test/matcher/toDeepEqual";
import * as cards from "api/services/cards";
import { SagaIterator } from "redux-saga";
import { call, put, select, take, takeEvery } from "redux-saga/effects";
import { selectUserId } from "store/user/selectors";
import { selectGroupId, selectSelectedCard } from "store/cards/selectors";
import { CardSummary } from "pages/cards/models";
import { deleteCardEffect, deleteCardWorker } from "../deleteCard";
import { deleteCardSuccess } from "store/cards/reducer";

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
    expect(saga.next().value).toStrictEqual(takeEvery("cards/deleteCard", deleteCardWorker));
  });

  // it("should go through", () => {
  //   const userId = "userId";
  //   const groupId = "groupId";
  //   const selectedItem: CardSummary = {
  //     id: "cardId",
  //     front: {} as any,
  //     back: {} as any,
  //   };
  //   const response = {};
  //   expect(saga.next().value).toStrictEqual(take("cards/deleteCard"));
  //   expect(saga.next().value).toStrictEqual(select(selectUserId));
  //   expect(saga.next(userId).value).toStrictEqual(select(selectGroupId));
  //   expect(saga.next(groupId).value).toStrictEqual(select(selectSelectedCard));
  //   expect(saga.next(selectedItem).value).toStrictEqual(
  //     call(mock, userId, groupId, selectedItem.id)
  //   );
  //   expect(saga.next(response).value).toDeepEqual(
  //     put(deleteCardSuccess({ cardId: selectedItem.id }))
  //   );
  // });
});
