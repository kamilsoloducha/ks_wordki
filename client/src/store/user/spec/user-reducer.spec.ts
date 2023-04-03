import * as mocks from "./user-reducer.mocks.spec";
import userReducer from "../reducer";

describe("userReducer", () => {
  [
    mocks.defaultCtx,
    mocks.loginCtx,
    mocks.loginSuccessCtx,
    mocks.logoutCtx,
    mocks.registerCtx,
    mocks.setErrorMessageCtx,
  ].forEach((item, index) => {
    it("should reduce action " + index, () => {
      const result = userReducer(item.initialState, item.action);
      expect(result).toStrictEqual(item.resultState);
    });
  });
});
