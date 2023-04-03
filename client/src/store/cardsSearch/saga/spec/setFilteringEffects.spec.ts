import "test/matcher/toDeepEqual";
import { SagaIterator } from "redux-saga";
import { put } from "redux-saga/effects";
import { filterWorker } from "../setFilteringEffects";
import { search } from "store/cardsSearch/reducer";

describe("setPaginationEffect", () => {
  let saga: SagaIterator;

  beforeEach(() => {
    saga = filterWorker();
  });

  afterEach(() => {
    jest.restoreAllMocks();
  });

  it("should go through", () => {
    expect(saga.next().value).toDeepEqual(put(search()));
  });
});
