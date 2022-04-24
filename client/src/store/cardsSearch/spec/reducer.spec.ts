import * as mocks from "./reducer.mocks.spec";
import reducer from "../reducer";

describe("cardsSearch reducer", () => {
  [
    new mocks.GetOverviewCtx(),
    new mocks.GetOverviewSuccessCtx(),
    new mocks.SearchCtx(),
    new mocks.SearchSuccessCtx(),
    new mocks.FilterResetCtx(),
    new mocks.FilterSetLessonIncludedCtx(),
    new mocks.FilterSetPaginationCtx(),
    new mocks.FilterSetTermCtx(),
    new mocks.FilterSetTickedCtx(),
    new mocks.DeleteCardCtx(),
    new mocks.UpdateCardCtx(),
    new mocks.UpdateCardSuccessCtx(),
  ].forEach((item) => {
    it("should return proper value :: " + item.constructor.name, () => {
      const result = reducer(item.givenState, item.action);
      expect(result).toStrictEqual(item.expectedResult);
    });
  });
});
