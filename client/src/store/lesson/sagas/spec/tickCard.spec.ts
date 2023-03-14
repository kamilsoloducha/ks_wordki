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
      sideType: 1,
      cardId: "cardId",
      question: "",
      questionExample: "",
      questionDrawer: 0,
      answer: "",
      answerExample: "",
      questionLanguage: "",
      answerLanguage: "",
    };
    const request: api.TickCardRequest = { cardId: "cardId" };
    expect(saga.next().value).toStrictEqual(take("lesson/tickCard"));
    expect(saga.next().value).toStrictEqual(select(selectUserId));
    expect(saga.next(userId).value).toStrictEqual(select(selectCurrectRepeat));
    expect(saga.next(repeat).value).toStrictEqual(call(api.tickCard, request));
  });
});
