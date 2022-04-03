import { getLearningCardCount } from "../learningCardsCounter";
import * as mocks from "./learningCardsCounter.mocks.spec";

describe("getLearningCardCount", () => {
  [
    new mocks.EmptyList(),
    new mocks.SingleItemSingleSideList(),
    new mocks.SingleItemDoubleSideList(),
    new mocks.MultipeItemsDoubleSideList(),
  ].forEach((item) => {
    it("should return proper value ::" + item.constructor.name, () => {
      var result = getLearningCardCount(item.cards);
      expect(result).toBe(item.result);
    });
  });
});
