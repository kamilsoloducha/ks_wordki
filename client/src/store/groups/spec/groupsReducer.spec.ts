import * as mocks from "./groupsReducer.mocks.spec";
import reducer from "../reducer";

describe("groupsReducer", () => {
  [
    new mocks.AddGroup(),
    new mocks.AddGroupSuccess(),
    new mocks.GetGroupsSummary(),
    new mocks.GetGroupsSummarySuccess(),
    new mocks.ResetSelectedItem(),
    new mocks.SelectItem(),
    new mocks.SelectItemById(),
    new mocks.UpdateGroup(),
    new mocks.UpdateGroupSuccess(),
  ].forEach((item) => {
    it("should return proper value :: " + item.constructor.name, () => {
      const result = reducer(item.givenState, item.givenAction);
      expect(result).toStrictEqual(item.expectedState);
    });
  });
});
