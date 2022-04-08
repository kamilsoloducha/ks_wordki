import { call, put, select, take } from "@redux-saga/core/effects";
import { requestFailed } from "store/root/actions";
import { selectUserId } from "store/user/selectors";
import { GroupsActionEnum, UpdateGroup } from "../actions";
import * as api from "api";
import { ApiResponse } from "common/models/response";
import { getCards } from "store/cards/actions";
import { SagaIterator } from "redux-saga";

export function* updateGroupEffect(): SagaIterator {
  const { group }: UpdateGroup = yield take(GroupsActionEnum.UPDATE_GROUP);
  const userId: string = yield select(selectUserId);

  const request = {
    userId,
    groupId: group.id,
    groupName: group.name,
    back: group.back,
    front: group.front,
  } as api.UpdateGroupRequest;
  const { data, error }: { data: ApiResponse<string>; error: any } = yield call(() =>
    api.updateGroup(request)
  );
  yield put(data ? getCards(group.id) : requestFailed(error));
}
