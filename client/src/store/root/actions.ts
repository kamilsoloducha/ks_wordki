import RootState from "./state";
import * as url from "history";

export enum RootActionEnum {
  REQUEST_FAILED = "[ROOT] REQUEST_FAILED",
}

export interface RootAction {
  type: RootActionEnum;
  reduce: (state: RootState) => RootState;
}

export interface RequestFailed extends RootAction {
  error: Error;
}

export function requestFailed(error: Error): RequestFailed {
  console.error(error);
  return {
    error,
    type: RootActionEnum.REQUEST_FAILED,
    reduce: (state: RootState): RootState => {
      const history = url.createBrowserHistory();
      history.push("/error");
      return { ...state };
    },
  };
}
