import "test/matcher/toDeepEqual";
import * as groups from "api/services/groups";
import * as api from "api";
import { SagaIterator } from "redux-saga";
import { call, put, select, take, takeEvery } from "redux-saga/effects";
import { selectUserId } from "store/user/selectors";
import { addGroupEffect, addGroupWorker } from "../addGroup";
import { addGroup, getGroupsSummary } from "store/groups/reducer";

describe("addGroupEffect", () => {
  let saga: SagaIterator;
  let apiMock: any;

  beforeEach(() => {
    apiMock = jest.spyOn(groups, "addGroup");
    saga = addGroupEffect();
  });

  afterEach(() => {
    jest.restoreAllMocks();
  });

  it("should go through", () => {
    expect(saga.next().value).toStrictEqual(takeEvery("groups/addGroup", addGroupWorker));
  });

  // it("should go through", () => {
  //   const action = addGroup({ group: { id: "id", name: "name" } as any });
  //   const userId = "userId";
  //   const request = {
  //     userId,
  //     groupName: action.payload.group.name,
  //     back: action.payload.group.back,
  //     front: action.payload.group.front,
  //   } as api.AddGroupRequest;
  //   const response = { data: {} };

  //   expect(saga.next().value).toStrictEqual(take("groups/addGroup"));
  //   expect(saga.next(action).value).toStrictEqual(select(selectUserId));
  //   expect(saga.next(userId).value).toStrictEqual(call(apiMock, request));
  //   expect(saga.next(response).value).toDeepEqual(put(getGroupsSummary()));
  // });
});
