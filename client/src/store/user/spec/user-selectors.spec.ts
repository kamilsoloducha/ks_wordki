import { MainState } from "store/store";
import { selectIsLogin } from "../selectors";

const initialMainState = {
  userReducer: {
    expirationDate: new Date(),
    id: "test",
    isLoading: false,
    isLogin: false,
    token: "token",
  },
};

interface Context<T> {
  state: any;
  result: T;
  selector: (state: MainState) => T;
}

const selectIsLoginCtx: Context<boolean> = {
  state: initialMainState,
  result: false,
  selector: selectIsLogin,
};

describe("userSelector", () => {
  [selectIsLoginCtx].forEach((item, index) => {
    it("should return proper value :: " + index, () => {
      const result = item.selector(item.state);
      expect(result).toStrictEqual(item.result);
    });
  });
});
