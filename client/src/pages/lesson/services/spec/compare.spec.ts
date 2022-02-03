import { compare } from "../compare";

describe("answerComparer", () => {
  [
    { text1: "test", text2: "test", result: true },
    { text1: "asdf", text2: "fdsa", result: false },
  ].forEach((item) => {
    it("should compare texts correctly", () => {
      const result = compare(item.text1, item.text2);
      expect(result).toBe(item.result);
    });
  });
});
