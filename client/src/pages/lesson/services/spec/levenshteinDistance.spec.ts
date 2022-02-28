import * as leven from "../levenshteinDistance";

const context1 = {
  text1: "aa",
  text2: "aa",
  result: [
    { char: "a", type: leven.Equal },
    { char: "a", type: leven.Equal },
  ],
};

const context2 = {
  text1: "aa",
  text2: "aaa",
  result: [
    { char: "a", type: leven.Insert },
    { char: "a", type: leven.Equal },
    { char: "a", type: leven.Equal },
  ],
};

const context3 = {
  text1: "aaa",
  text2: "aa",
  result: [
    { char: "a", type: leven.Delete },
    { char: "a", type: leven.Equal },
    { char: "a", type: leven.Equal },
  ],
};

const context4 = {
  text1: "abc",
  text2: "abq",
  result: [
    { char: "a", type: leven.Equal },
    { char: "b", type: leven.Equal },
    { char: "c", type: leven.Replace },
  ],
};

const context5 = {
  text1: "test test",
  text2: "test a test",
  result: [
    { char: "t", type: leven.Equal },
    { char: "e", type: leven.Equal },
    { char: "s", type: leven.Equal },
    { char: "t", type: leven.Equal },
    { char: " ", type: leven.Insert },
    { char: "a", type: leven.Insert },
    { char: " ", type: leven.Equal },
    { char: "t", type: leven.Equal },
    { char: "e", type: leven.Equal },
    { char: "s", type: leven.Equal },
    { char: "t", type: leven.Equal },
  ],
};

const context6 = {
  text1: "test test",
  text2: "tqst a tet",
  result: [
    { char: "t", type: leven.Equal },
    { char: "e", type: leven.Replace },
    { char: "s", type: leven.Equal },
    { char: "t", type: leven.Equal },
    { char: " ", type: leven.Insert },
    { char: "a", type: leven.Insert },
    { char: " ", type: leven.Equal },
    { char: "t", type: leven.Equal },
    { char: "e", type: leven.Equal },
    { char: "s", type: leven.Delete },
    { char: "t", type: leven.Equal },
  ],
};

fdescribe("convertLevenshteinTable", () => {
  [context1, context2, context3, context4, context5, context6].forEach((item) => {
    it("should return properValue", () => {
      const result = leven.levenshtein(item.text1, item.text2);
      expect(result).toStrictEqual(item.result);
    });
  });
});
