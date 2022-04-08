import "test/matcher/toDeepEqual";
import * as actions from "../../actions";
import * as api from "api";
import { call, select, take } from "redux-saga/effects";
import { selectUserId } from "store/user/selectors";
import { FormModel } from "common/components/dialogs/cardDialog/CardForm";
import { updateCardEffect } from "../updateCard";

describe("updateCardEffect", () => {
  it("should go through", () => {
    const userId = "userId";
    const form: FormModel = {
      cardId: "cardId",
      frontValue: "frontValue",
      frontExample: "frontExample",
      frontEnabled: true,
      backValue: "backValue",
      backExample: "backExample",
      backEnabled: true,
      comment: "comment",
      isTicked: true,
    };
    const updateCardAction = actions.updateCard(form, "groupId");
    const saga = updateCardEffect();
    const request: api.UpdateCardRequest = {
      userId: "userId",
      groupId: "groupId",
      cardId: "cardId",
      front: {
        value: "frontValue",
        example: "frontExample",
        isUsed: true,
        isTicked: true,
      },
      back: {
        value: "backValue",
        example: "backExample",
        isUsed: true,
        isTicked: true,
      },
    };
    expect(saga.next().value).toStrictEqual(take(actions.DailyActionEnum.UPDATE_CARD));
    expect(saga.next(updateCardAction).value).toStrictEqual(select(selectUserId));
    expect(saga.next("userId").value).toStrictEqual(call(api.updateCard2, request));
  });
});
