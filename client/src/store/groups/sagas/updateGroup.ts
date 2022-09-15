import { call, put, select, take } from "@redux-saga/core/effects";
import { requestFailed } from "store/root/actions";
import { selectUserId } from "store/user/selectors";
import * as api from "api";
import { ApiResponse } from "common/models/response";
import { SagaIterator } from "redux-saga";
import { getCards } from "store/cards/reducer";
import { PayloadAction } from "@reduxjs/toolkit";
import { UpdateGroup } from "../action-payloads";

export function* updateGroupEffect(): SagaIterator {
  while (true) {
    const action: PayloadAction<UpdateGroup> = yield take("groups/updateGroup");
    const userId: string = yield select(selectUserId);

    const request = {
      userId,
      groupId: action.payload.group.id,
      groupName: action.payload.group.name,
      back: action.payload.group.back,
      front: action.payload.group.front,
    } as api.UpdateGroupRequest;
    const { data, error }: { data: ApiResponse<string>; error: any } = yield call(
      api.updateGroup,
      request
    );
    yield put(data ? getCards({ groupId: action.payload.group.id }) : requestFailed(error));
  }
}