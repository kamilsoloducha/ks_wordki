import UserState from "../state";
import userReducer from "../reducer";
import * as actions from "../actions";

const initialState: UserState = {
  expirationDate: new Date(),
  id: "test",
  isLoading: false,
  isLogin: false,
  token: "token",
};

interface Context {
  initialState: UserState;
  action: actions.UserAction;
  resultState: UserState;
}

const getLoginUser: Context = {
  initialState: { ...initialState },
  action: actions.getLoginUser("name", "password"),
  resultState: {
    ...initialState,
    isLoading: true,
  },
};

describe("userReducer", () => {
  [getLoginUser].forEach((item, index) => {
    it("should reduce action " + index, () => {
      var result = userReducer(item.initialState, item.action);
      expect(result).toStrictEqual(item.resultState);
    });
  });
});
