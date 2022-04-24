import "test/matcher/toDeepEqual";
import * as api from "api";
import { call, select, take } from "redux-saga/effects";
import { selectUserId } from "store/user/selectors";
import { selectCurrectRepeat } from "store/lesson/selectors";
import { Repeat } from "pages/lesson/models/repeat";
import { tickCardEffect } from "../tickCard";

describe("tickCardEffect", () => {
  let saga: any;

  beforeEach(() => {
    saga = tickCardEffect();
  });

  afterEach(() => {
    jest.restoreAllMocks();
  });

  it("should go through", () => {
    const userId = "userId";
    const repeat: Repeat = {
      sideId: "sideId",
      cardId: "",
      questionSide: 0,
      question: "",
      questionExample: "",
      questionDrawer: 0,
      answer: "",
      answerExample: "",
      answerSide: 0,
      frontLanguage: 0,
      backLanguage: 0,
      comment: "",
      groupId: "",
    };
    const request: api.TickCardRequest = { userId: "userId", sideId: "sideId" };
    expect(saga.next().value).toStrictEqual(take("lesson/tickCard"));
    expect(saga.next().value).toStrictEqual(select(selectUserId));
    expect(saga.next(userId).value).toStrictEqual(select(selectCurrectRepeat));
    expect(saga.next(repeat).value).toStrictEqual(call(api.tickCard, request));
  });
});
