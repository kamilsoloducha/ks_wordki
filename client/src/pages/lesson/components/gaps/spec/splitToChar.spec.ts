import { splitToChars } from "../Gaps";

fdescribe("splitToChars", () => {
  [
    {
      givenText: "aaa",
      expectation: new Map<string, number>([["a", 3]]),
    },
    {
      givenText: "aaa aaa",
      expectation: new Map<string, number>([["a", 6]]),
    },
    {
      givenText: "a.,!?/|@#$%^&*()",
      expectation: new Map<string, number>([["a", 1]]),
    },
    {
      givenText: "Aa1",
      expectation: new Map<string, number>([
        ["a", 1],
        ["A", 1],
        ["1", 1],
      ]),
    },
    {
      givenText: "test test",
      expectation: new Map<string, number>([
        ["t", 4],
        ["e", 2],
        ["s", 2],
      ]),
    },
  ].forEach((item, index) => {
    it(`should split text to chars : ${index}`, () => {
      const result = splitToChars(item.givenText);
      expect(result).toEqual(item.expectation);
    });
  });
});
