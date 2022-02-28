import { AnswerLetter2, getAnswerLettersAdvanced } from "../getAnswerLettersAdvanced";

interface Context {
  givenCorrectAnswer: string;
  givenUserAnswer: string;
  expectedAnswerLetters: AnswerLetter2[];
}

const singleCorrectWord: Context = {
  givenCorrectAnswer: "test",
  givenUserAnswer: "test",
  expectedAnswerLetters: [
    { letter: "t", status: 0 },
    { letter: "e", status: 0 },
    { letter: "s", status: 0 },
    { letter: "t", status: 0 },
  ],
};

const singleWordChangedOneLetter: Context = {
  givenCorrectAnswer: "test",
  givenUserAnswer: "teqt",
  expectedAnswerLetters: [
    { letter: "t", status: 0 },
    { letter: "e", status: 0 },
    { letter: "s", status: 4 },
    { letter: "t", status: 0 },
  ],
};

const singleWordChangedMoreLetters: Context = {
  givenCorrectAnswer: "smalltest",
  givenUserAnswer: "swalltdat",
  expectedAnswerLetters: [
    { letter: "s", status: 0 },
    { letter: "m", status: 4 },
    { letter: "a", status: 0 },
    { letter: "l", status: 0 },
    { letter: "l", status: 0 },
    { letter: "t", status: 0 },
    { letter: "e", status: 4 },
    { letter: "s", status: 4 },
    { letter: "t", status: 0 },
  ],
};

const singleWordAdditionalSingleLetter: Context = {
  givenCorrectAnswer: "smalltest",
  givenUserAnswer: "smallatest",
  expectedAnswerLetters: [
    { letter: "s", status: 0 },
    { letter: "m", status: 0 },
    { letter: "a", status: 0 },
    { letter: "l", status: 0 },
    { letter: "l", status: 0 },
    { letter: "a", status: 4 },
    { letter: "t", status: 0 },
    { letter: "e", status: 0 },
    { letter: "s", status: 0 },
    { letter: "t", status: 0 },
  ],
};

const singleWordAdditionalMoreLetters: Context = {
  givenCorrectAnswer: "smalltest",
  givenUserAnswer: "smaallatesst",
  expectedAnswerLetters: [
    { letter: "s", status: 0 },
    { letter: "m", status: 0 },
    { letter: "a", status: 0 },
    { letter: "a", status: 4 },
    { letter: "l", status: 0 },
    { letter: "l", status: 0 },
    { letter: "a", status: 4 },
    { letter: "t", status: 0 },
    { letter: "e", status: 0 },
    { letter: "s", status: 0 },
    { letter: "s", status: 4 },
    { letter: "t", status: 0 },
  ],
};

const singleWordAdditionalMoreLettersCustom1: Context = {
  givenCorrectAnswer: "smalltest",
  givenUserAnswer: "stmalltest",
  expectedAnswerLetters: [
    { letter: "s", status: 0 },
    { letter: "t", status: 4 },
    { letter: "m", status: 0 },
    { letter: "a", status: 0 },
    { letter: "a", status: 4 },
    { letter: "l", status: 0 },
    { letter: "l", status: 0 },
    { letter: "t", status: 0 },
    { letter: "e", status: 0 },
    { letter: "s", status: 0 },
    { letter: "t", status: 0 },
  ],
};

const singleWordAdditionalSingleLetterWithSwap: Context = {
  givenCorrectAnswer: "smalltest",
  givenUserAnswer: "samllatest",
  expectedAnswerLetters: [
    { letter: "s", status: 0 },
    { letter: "m", status: 4 },
    { letter: "a", status: 4 },
    { letter: "l", status: 0 },
    { letter: "l", status: 0 },
    { letter: "a", status: 4 },
    { letter: "t", status: 0 },
    { letter: "e", status: 0 },
    { letter: "s", status: 0 },
    { letter: "t", status: 0 },
  ],
};

const singleWordAdditionalMoreLettersWithSwap: Context = {
  givenCorrectAnswer: "smalltest",
  givenUserAnswer: "samllatesst",
  expectedAnswerLetters: [
    { letter: "s", status: 0 },
    { letter: "m", status: 4 },
    { letter: "a", status: 4 },
    { letter: "l", status: 0 },
    { letter: "l", status: 0 },
    { letter: "a", status: 4 },
    { letter: "t", status: 0 },
    { letter: "e", status: 0 },
    { letter: "s", status: 0 },
    { letter: "s", status: 4 },
    { letter: "t", status: 0 },
  ],
};

const singleWordMissingSingleLetter: Context = {
  givenCorrectAnswer: "smalltest",
  givenUserAnswer: "smaltest",
  expectedAnswerLetters: [
    { letter: "s", status: 0 },
    { letter: "m", status: 0 },
    { letter: "a", status: 0 },
    { letter: "l", status: 0 },
    { letter: "l", status: 4 },
    { letter: "t", status: 0 },
    { letter: "e", status: 0 },
    { letter: "s", status: 0 },
    { letter: "t", status: 0 },
  ],
};

const singleWordMissingMoreLetters: Context = {
  givenCorrectAnswer: "smalltest",
  givenUserAnswer: "smaltet",
  expectedAnswerLetters: [
    { letter: "s", status: 0 },
    { letter: "m", status: 0 },
    { letter: "a", status: 0 },
    { letter: "l", status: 0 },
    { letter: "l", status: 4 },
    { letter: "t", status: 0 },
    { letter: "e", status: 0 },
    { letter: "s", status: 4 },
    { letter: "t", status: 0 },
  ],
};

const singleWordMissingMoreLettersInRow: Context = {
  givenCorrectAnswer: "smalltest",
  givenUserAnswer: "smatest",
  expectedAnswerLetters: [
    { letter: "s", status: 0 },
    { letter: "m", status: 0 },
    { letter: "a", status: 0 },
    { letter: "l", status: 4 },
    { letter: "l", status: 4 },
    { letter: "t", status: 0 },
    { letter: "e", status: 0 },
    { letter: "s", status: 0 },
    { letter: "t", status: 0 },
  ],
};

const singleWordMissingMoreLettersWithSwap: Context = {
  givenCorrectAnswer: "smalltest",
  givenUserAnswer: "samtest",
  expectedAnswerLetters: [
    { letter: "s", status: 0 },
    { letter: "m", status: 4 },
    { letter: "a", status: 4 },
    { letter: "l", status: 4 },
    { letter: "l", status: 4 },
    { letter: "t", status: 0 },
    { letter: "e", status: 0 },
    { letter: "s", status: 0 },
    { letter: "t", status: 0 },
  ],
};

const multipleWords: Context = {
  givenCorrectAnswer: "small test",
  givenUserAnswer: "small test",
  expectedAnswerLetters: [
    { letter: "s", status: 0 },
    { letter: "m", status: 0 },
    { letter: "a", status: 0 },
    { letter: "l", status: 0 },
    { letter: "l", status: 0 },
    { letter: " ", status: 0 },
    { letter: "t", status: 0 },
    { letter: "e", status: 0 },
    { letter: "s", status: 0 },
    { letter: "t", status: 0 },
  ],
};

const multipleWordsAdditionalLetters: Context = {
  givenCorrectAnswer: "small test",
  givenUserAnswer: "smaall tesqt",
  expectedAnswerLetters: [
    { letter: "s", status: 0 },
    { letter: "m", status: 0 },
    { letter: "a", status: 0 },
    { letter: "a", status: 4 },
    { letter: "l", status: 0 },
    { letter: "l", status: 0 },
    { letter: " ", status: 0 },
    { letter: "t", status: 0 },
    { letter: "e", status: 0 },
    { letter: "s", status: 0 },
    { letter: "q", status: 4 },
    { letter: "t", status: 0 },
  ],
};

const multipleWordsMissingWord: Context = {
  givenCorrectAnswer: "a small test",
  givenUserAnswer: "small test",
  expectedAnswerLetters: [
    { letter: "a", status: 4 },
    { letter: " ", status: 4 },
    { letter: "s", status: 0 },
    { letter: "m", status: 0 },
    { letter: "a", status: 0 },
    { letter: "a", status: 4 },
    { letter: "l", status: 0 },
    { letter: "l", status: 0 },
    { letter: " ", status: 0 },
    { letter: "t", status: 0 },
    { letter: "e", status: 0 },
    { letter: "s", status: 0 },
    { letter: "q", status: 4 },
    { letter: "t", status: 0 },
  ],
};

describe("getAnswerLettersAdvanced", () => {
  [
    singleCorrectWord,
    singleWordChangedOneLetter,
    singleWordChangedMoreLetters,
    singleWordAdditionalSingleLetter,
    singleWordAdditionalMoreLetters,
    singleWordAdditionalSingleLetterWithSwap,
    singleWordAdditionalMoreLettersWithSwap,
    singleWordMissingSingleLetter,
    singleWordMissingMoreLetters,
    singleWordMissingMoreLettersInRow,
    singleWordMissingMoreLettersWithSwap,

    multipleWords,
    multipleWordsAdditionalLetters,
    // singleWordAdditionalMoreLettersCustom1,
    // multipleWordsMissingWord,
  ].forEach((item, index) => {
    it("should prepare answer letters :: " + index, () => {
      const result = getAnswerLettersAdvanced(item.givenCorrectAnswer, item.givenUserAnswer);
      expect(result).toStrictEqual(item.expectedAnswerLetters);
    });
  });
});
