import * as mocks from "./groupsSearchReducer.mocks.spec";
import reducer from "../reducer";

describe("groupsSearchReducer", () => {
  [
    new mocks.FiterSetName(),
    new mocks.GetCards(),
    new mocks.GetCardsSuccess(),
    new mocks.ResetSelection(),
    new mocks.SaveGroup(),
    new mocks.SaveGroupSuccess(),
    new mocks.Search(),
    new mocks.SearchSuccess(),
    new mocks.SetGroup(),
  ].forEach((item) => {
    it("should return proper value :: " + item.constructor.name, () => {
      const result = reducer(item.givenState, item.givenAction);
      expect(result).toStrictEqual(item.expectedResult);
    });
  });
});
