import { call, put, select } from "@redux-saga/core/effects";
import { requestFailed } from "store/root/actions";
import { selectUserId } from "store/user/selectors";
import * as api from "api";
import { ApiResponse } from "common/models/response";
import { take } from "redux-saga/effects";
import { SagaIterator } from "redux-saga";
import { PayloadAction } from "@reduxjs/toolkit";
import { AddGroup } from "../action-payloads";
import { getGroupsSummary } from "../reducer";

export function* addGroupEffect(): SagaIterator {
  while (true) {
    const action: PayloadAction<AddGroup> = yield take("groups/addGroup");
    const userId: string = yield select(selectUserId);

    const request = {
      userId,
      groupName: action.payload.group.name,
      back: action.payload.group.back,
      front: action.payload.group.front,
    } as api.AddGroupRequest;
    const { data, error }: { data: ApiResponse<string>; error: any } = yield call(
      api.addGroup,
      request
    );
    yield put(data ? getGroupsSummary() : requestFailed(error));
  }
}