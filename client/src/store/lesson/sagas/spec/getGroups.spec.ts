import "test/matcher/toDeepEqual";
import * as groups from "api/services/groups";
import { call, put } from "redux-saga/effects";
import { getGroup } from "../getGroups";
import { getGroupsSuccess } from "store/lesson/reducer";
import { Group } from "pages/lessonSettings/models/group";

describe("getGroupsEffect", () => {
  let saga: any;
  let mock: any;

  beforeEach(() => {
    mock = jest.spyOn(groups, "getGroups");
    saga = getGroup();
  });

  afterEach(() => {
    jest.restoreAllMocks();
  });

  it("should retrun getGroupsSuccess if response is correct", () => {
    const response = [] as Group[];

    expect(saga.next().value).toStrictEqual(call(mock));
    expect(saga.next(response).value).toDeepEqual(put(getGroupsSuccess({ groups: [] })));
  });
});
