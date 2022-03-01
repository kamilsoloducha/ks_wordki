import RootState, { Breadcrumb } from "./state";
import { Action } from "@reduxjs/toolkit";

export enum RootActionEnum {
  REQUEST_FAILED = "[ROOT] REQUEST_FAILED",
  SET_BREADCRUMBS = "[ROOT] SET_BREADCRUMBS",
}

export interface RequestFailed extends Action {
  error: Error;
}
export function requestFailed(error: Error): RequestFailed {
  const action: RequestFailed = {
    type: RootActionEnum.REQUEST_FAILED,
    error,
  };
  return action;
}

export function requestFailedReduce(state: RootState, _: RequestFailed): RootState {
  return { ...state };
}

export interface SetBreadcrumbs extends Action {
  breadcrumbs: Breadcrumb[];
}
export function setBreadcrumbs(breadcrumbs: Breadcrumb[]): SetBreadcrumbs {
  const action: SetBreadcrumbs = {
    type: RootActionEnum.SET_BREADCRUMBS,
    breadcrumbs,
  };
  return action;
}
export function setBreadcrumbsReduce(state: RootState, action: SetBreadcrumbs): RootState {
  return {
    ...state,
    breadcrumbs: action.breadcrumbs,
  };
}

export type RootActions = SetBreadcrumbs | RequestFailed;
