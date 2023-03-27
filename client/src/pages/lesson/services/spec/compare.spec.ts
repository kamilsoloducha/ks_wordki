import { compare } from "../compare";

describe("answerComparer", () => {
  [
    { text1: "asdf1;asdf2", text2: "asdf", result: false },
    { text1: "asdf;asdf", text2: "asdf", result: true },
    { text1: "asdf;asdf", text2: "asdf ", result: true },
    { text1: "test", text2: "test", result: true },
    { text1: "asdf", text2: "fdsa", result: false },
    { text1: "test;asdf", text2: "test", result: true },
    { text1: "asdf;test", text2: "test", result: true },
    { text1: "asdf ; test", text2: "test", result: true },
  ].forEach((item) => {
    it("should compare texts correctly", () => {
      const result = compare(item.text1, item.text2);
      expect(result).toBe(item.result);
    });
  });
});
