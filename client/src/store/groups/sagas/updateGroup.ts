import { call, put, select } from "@redux-saga/core/effects";
import { requestFailed } from "store/root/actions";
import { selectUserId } from "store/user/selectors";
import * as api from "api";
import { ApiResponse } from "common/models/response";
import { SagaIterator } from "redux-saga";
import { getCards } from "store/cards/reducer";
import { PayloadAction } from "@reduxjs/toolkit";
import { UpdateGroup } from "../action-payloads";
import { takeEvery } from "redux-saga/effects";

export function* updateGroupWorker(action: PayloadAction<UpdateGroup>): any {

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

export function* updateGroupEffect(): SagaIterator {
  yield takeEvery("groups/updateGroup", updateGroupWorker);
}