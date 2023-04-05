import "test/matcher/toDeepEqual";
import * as cards from "api/services/cards";
import { SagaIterator } from "redux-saga";
import { call, put } from "redux-saga/effects";
import { search, updateCard } from "store/cardsSearch/reducer";
import { updateCardWorker } from "../updateCardEffect";
import { UpdateCardRequest } from "api";

describe("updateCardEffect", () => {
  afterEach(() => {
    jest.restoreAllMocks();
  });
});
