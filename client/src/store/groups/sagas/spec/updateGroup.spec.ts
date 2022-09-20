import "test/matcher/toDeepEqual";
import * as groups from "api/services/groups";
import * as api from "api";
import { SagaIterator } from "redux-saga";
import { call, put, select, take, takeEvery } from "redux-saga/effects";
import { selectUserId } from "store/user/selectors";
import { addGroupEffect } from "../addGroup";
import {
  addGroup,
  getGroupsSummary,
  getGroupsSummarySuccess,
  updateGroup,
} from "store/groups/reducer";
import { getGroupsSummaryEffect } from "../getGroupsSummary";
import { updateGroupEffect, updateGroupWorker } from "../updateGroup";
import { getCards } from "store/cards/reducer";

describe("updateGroupEffect", () => {
  let saga: SagaIterator;
  let apiMock: any;

  beforeEach(() => {
    apiMock = jest.spyOn(groups, "updateGroup");
    saga = updateGroupEffect();
  });

  afterEach(() => {
    jest.restoreAllMocks();
  });

  it("should go through", () => {
    expect(saga.next().value).toStrictEqual(takeEvery("groups/updateGroup", updateGroupWorker));
  });

  // it("should go through", () => {
  //   const action = updateGroup({
  //     group: { id: "id", name: "name", back: {} as any, front: {} as any },
  //   });
  //   const userId = "userId";
  //   const request = {
  //     userId,
  //     groupId: action.payload.group.id,
  //     groupName: action.payload.group.name,
  //     back: action.payload.group.back,
  //     front: action.payload.group.front,
  //   } as api.UpdateGroupRequest;
  //   const response = { data: { groups: [] } };

  //   expect(saga.next().value).toStrictEqual(take("groups/updateGroup"));
  //   expect(saga.next(action).value).toStrictEqual(select(selectUserId));
  //   expect(saga.next(userId).value).toStrictEqual(call(apiMock, request));
  //   expect(saga.next(response).value).toDeepEqual(put(getCards({ groupId: "id" })));
  // });
});
