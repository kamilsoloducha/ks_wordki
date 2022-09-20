import "test/matcher/toDeepEqual";
import * as groups from "api/services/groups";
import * as api from "api";
import { SagaIterator } from "redux-saga";
import { call, put, select, take, takeEvery } from "redux-saga/effects";
import { selectUserId } from "store/user/selectors";
import { addGroupEffect } from "../addGroup";
import { addGroup, getGroupsSummary, getGroupsSummarySuccess } from "store/groups/reducer";
import { getGroupsSummaryEffect, getGroupsSummaryWorker } from "../getGroupsSummary";

describe("getGroupsSummaryEffect", () => {
  let saga: SagaIterator;
  let apiMock: any;

  beforeEach(() => {
    apiMock = jest.spyOn(groups, "groups");
    saga = getGroupsSummaryEffect();
  });

  afterEach(() => {
    jest.restoreAllMocks();
  });

  // it("should go through", () => {
  //   const userId = "userId";
  //   const response = { data: { groups: [] } };

  //   expect(saga.next().value).toStrictEqual(take("groups/getGroupsSummary"));
  //   expect(saga.next().value).toStrictEqual(select(selectUserId));
  //   expect(saga.next(userId).value).toStrictEqual(call(apiMock, userId));
  //   expect(saga.next(response).value).toDeepEqual(put(getGroupsSummarySuccess({ groups: [] })));
  // });

  it("should go through", () => {
    expect(saga.next().value).toStrictEqual(takeEvery("groups/getGroupsSummary", getGroupsSummaryWorker));
  });
});
