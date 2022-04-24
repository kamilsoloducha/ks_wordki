import RootState, { initialState } from "./state";
import * as actions from "./actions";
import { Action } from "@reduxjs/toolkit";

export default function rootReducer(state = initialState, action: Action): RootState {
  switch (action.type) {
    case actions.RootActionEnum.REQUEST_FAILED:
      return actions.requestFailedReduce(state, action as actions.RequestFailed);
    case actions.RootActionEnum.SET_BREADCRUMBS:
      return actions.setBreadcrumbsReduce(state, action as actions.SetBreadcrumbs);
    default:
      return state;
  }
}
