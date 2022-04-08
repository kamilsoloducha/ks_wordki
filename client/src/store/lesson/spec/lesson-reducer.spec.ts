import lessonReducer from "../reducer";
import * as mocks from "./lesson-reducer.mocks.spec";

describe("lesson reducer", () => {
  [new mocks.ResetLessonCtx()].forEach((item) => {
    it("should reduce actions", () => {
      const result = lessonReducer(item.givenState, item.givenAction);
      expect(result).toStrictEqual(item.expectedState);
    });
  });
});
