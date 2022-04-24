import * as mocks from "./dashboardReducer.mocks.spec";
import reducer from "../reducer";

describe("dashboardReducer", () => {
  [
    new mocks.GetDashboardSummaryCtx(),
    new mocks.GetDashboardSummarySuccessCtx(),
    new mocks.GetForecastCtx(),
    new mocks.GetForecastSuccessCtx(),
  ].forEach((item) => {
    it("should return proper value :: " + item.constructor.name, () => {
      var result = reducer(item.givenState, item.action);
      expect(result).toStrictEqual(item.expectedResult);
    });
  });
});
