import { MainState } from "store/store";

export const selectToken = (state: MainState) => state.userReducer.token;
export const selectUserId = (state: MainState) => state.userReducer.id;
export const selectIsLogin = (state: MainState) => state.userReducer.isLogin;
export const selectIsLoading = (state: MainState) =>
  state.userReducer.isLoading;
