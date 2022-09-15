import "test/matcher/toDeepEqual";
import * as api from "api";
import * as groups from "api/services/groups";
import { call, put, select, take } from "redux-saga/effects";
import { selectUserId } from "store/user/selectors";
import { ApiResponse } from "common";
import { getGroupsEffect } from "../getGroups";
import { getGroupsSuccess } from "store/lesson/reducer";

describe("getGroupsEffect", () => {
  let saga: any;
  let mock: any;

  beforeEach(() => {
    mock = jest.spyOn(groups, "getGroups");
    saga = getGroupsEffect();
  });

  afterEach(() => {
    jest.restoreAllMocks();
  });

  it("should retrun getGroupsSuccess if response is correct", () => {
    const request: api.GetGroupsToLessonQuery = {
      ownerId: "ownerId",
    };
    const response: ApiResponse<api.GetGroupToLessonResponse> = {
      response: {
        groups: [],
      },
      error: "",
      isCorrect: false,
    };

    expect(saga.next().value).toStrictEqual(take("lesson/getGroups"));
    expect(saga.next().value).toStrictEqual(select(selectUserId));
    expect(saga.next("ownerId").value).toStrictEqual(call(mock, request));
    expect(saga.next(response).value).toDeepEqual(put(getGroupsSuccess({ groups: [] })));

    //expect(saga.next().done).toBe(true);
  });

  // it("test", () => {
  //   const request: api.GetGroupsToLessonQuery = {
  //     ownerId: "ownerId",
  //   };
  //   const response: ApiResponse<api.GetGroupToLessonResponse> = {
  //     response: {
  //       groups: [],
  //     },
  //     error: "",
  //     isCorrect: false,
  //   };
  //   mock = jest.spyOn(groups, "getGroups");
  //   mock.mockImplementation(
  //     () => new Promise<ApiResponse<api.GetGroupToLessonResponse>>(() => response)
  //   );

  //   testSaga(getGroups)
  //     .next()
  //     .select(selectUserId)
  //     .next("ownerId")
  //     .call(mock, {
  //       ownerId: "ownerId",
  //     })
  //     .next(response)
  //     .put(actions.getGroupsSuccess([]))
  //     .next()
  //     .isDone();
  // });
});
