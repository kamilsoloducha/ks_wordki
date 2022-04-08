import * as mocks from "./user-selectors.mocks.spec";

describe("userSelector", () => {
  [
    mocks.selectIsLoginCtx,
    mocks.selectErrorMessageCtx,
    mocks.selectIsLoadingCtx,
    mocks.selectTokenCtx,
    mocks.selectUserIdCtx,
  ].forEach((item, index) => {
    it("should return proper value :: " + index, () => {
      const result = item.selector(item.state);
      expect(result).toStrictEqual(item.result);
    });
  });
});
