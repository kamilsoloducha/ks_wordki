import { MainState } from "store/store";
import * as selectors from "../selectors";
import UserState from "../state";

const initialMainState: { userReducer: UserState } = {
  userReducer: {
    expirationDate: new Date(),
    id: "test",
    isLoading: true,
    isLogin: true,
    token: "token",
    errorMessage: "errorMessage",
  },
};

interface Context<T> {
  state: any;
  result: T;
  selector: (state: MainState) => T;
}

export const selectIsLoginCtx: Context<boolean> = {
  state: initialMainState,
  result: true,
  selector: selectors.selectIsLogin,
};

export const selectTokenCtx: Context<string> = {
  state: initialMainState,
  result: "token",
  selector: selectors.selectToken,
};

export const selectUserIdCtx: Context<string> = {
  state: initialMainState,
  result: "test",
  selector: selectors.selectUserId,
};

export const selectIsLoadingCtx: Context<boolean> = {
  state: initialMainState,
  result: true,
  selector: selectors.selectIsLoading,
};

export const selectErrorMessageCtx: Context<string> = {
  state: initialMainState,
  result: "errorMessage",
  selector: selectors.selectErrorMessage,
};
