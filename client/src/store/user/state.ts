export default interface UserState {
  isLogin: boolean;
  isLoading: boolean;
  token: string;
  id: string;
  expirationDate: Date;
}

export const initialState: UserState = {
  isLogin: false,
  isLoading: false,
  token: "",
  id: "",
  expirationDate: new Date(1),
};
