import { getCardsCountFromDrawerCount } from "../drawerCardsCounter";
import * as mocks from "./drawerCardsCounter.mocks.spec";

fdescribe("getCardsCountFromDrawerCount", () => {
  [
    new mocks.EmptyList(),
    new mocks.MultipleItemSingleSideList(),
    new mocks.SingleItemDoubleSideList(),
    new mocks.SingleItemSingleSideList(),
  ].forEach((item) => {
    it("should return proper value ::" + item.constructor.name, () => {
      const result = getCardsCountFromDrawerCount(item.cards, item.drawer);
      expect(result).toBe(item.result);
    });
  });
});
