export default interface UserState {
  isLogin: boolean;
  isLoading: boolean;
  token: string;
  id: string;
  expirationDate: string;
  errorMessage: string;
}

export const initialState: UserState = {
  isLogin: false,
  isLoading: false,
  token: "",
  id: "",
  expirationDate: "",
  errorMessage: "",
};
