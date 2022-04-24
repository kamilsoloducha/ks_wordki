import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import UserState, { initialState } from "./state";
import * as payload from "./action-payload";

export const userSlice = createSlice({
  name: "user",
  initialState: initialState,
  reducers: {
    login: (state: UserState, _: PayloadAction<payload.LoginPayload>): void => {
      state.isLoading = true;
    },
    loginSuccess: (state: UserState, action: PayloadAction<payload.LoginSuccessPayload>): void => {
      state.id = action.payload.id;
      state.token = action.payload.token;
      state.expirationDate = action.payload.expirationDate;
      state.isLoading = false;
      state.isLogin = true;
    },
    register: (state: UserState, _: PayloadAction<payload.RegisterPayload>): void => {
      state.isLoading = true;
    },
    logout: (state: UserState): void => {
      state.isLogin = false;
      state.isLoading = false;
      state.token = "";
      state.id = "";
      state.expirationDate = "";
      state.errorMessage = "";
    },
    setErrorMessage: (state: UserState, action: PayloadAction<string>): void => {
      state.isLoading = false;
      state.errorMessage = action.payload;
    },
  },
});

export default userSlice.reducer;

export const { login, loginSuccess, logout, register, setErrorMessage } = userSlice.actions;
