import "test/matcher/toDeepEqual";
import * as api from "api";
import { call, select } from "redux-saga/effects";
import { selectCurrectRepeat } from "store/lesson/selectors";
import { Repeat } from "pages/lesson/models/repeat";
import { tickCard } from "../tickCard";

describe("tickCardEffect", () => {
  let saga: any;

  beforeEach(() => {
    saga = tickCard();
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
    expect(saga.next(userId).value).toStrictEqual(select(selectCurrectRepeat));
    expect(saga.next(repeat).value).toStrictEqual(call(api.tickCard, "cardId"));
  });
});
