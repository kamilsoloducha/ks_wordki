import { AnswerLetter } from "../../models/answer";
import { getAnswerLetters } from "../getAnswerLetters";

interface Context {
  givenCorrectAnswer: string;
  givenUserAnswer: string;
  expectedAnswerLetters: AnswerLetter[];
}

const SimpleComparison = {
  givenCorrectAnswer: "test",
  givenUserAnswer: "test",
  expectedAnswerLetters: [
    { letter: "t", isAdditional: false, isCorrect: true } as AnswerLetter,
    { letter: "e", isAdditional: false, isCorrect: true } as AnswerLetter,
    { letter: "s", isAdditional: false, isCorrect: true } as AnswerLetter,
    { letter: "t", isAdditional: false, isCorrect: true } as AnswerLetter,
  ],
} as Context;

const SentenceComparison = {
  givenCorrectAnswer: "test asdf",
  givenUserAnswer: "test asdf",
  expectedAnswerLetters: [
    { letter: "t", isAdditional: false, isCorrect: true } as AnswerLetter,
    { letter: "e", isAdditional: false, isCorrect: true } as AnswerLetter,
    { letter: "s", isAdditional: false, isCorrect: true } as AnswerLetter,
    { letter: "t", isAdditional: false, isCorrect: true } as AnswerLetter,
    { letter: " ", isAdditional: false, isCorrect: true } as AnswerLetter,
    { letter: "a", isAdditional: false, isCorrect: true } as AnswerLetter,
    { letter: "s", isAdditional: false, isCorrect: true } as AnswerLetter,
    { letter: "d", isAdditional: false, isCorrect: true } as AnswerLetter,
    { letter: "f", isAdditional: false, isCorrect: true } as AnswerLetter,
  ],
} as Context;

const WrongLetter = {
  givenCorrectAnswer: "asd",
  givenUserAnswer: "aad",
  expectedAnswerLetters: [
    { letter: "a", isAdditional: false, isCorrect: true } as AnswerLetter,
    { letter: "s", isAdditional: false, isCorrect: false } as AnswerLetter,
    { letter: "d", isAdditional: false, isCorrect: true } as AnswerLetter,
  ],
} as Context;

const AdditionalLetter = {
  givenCorrectAnswer: "asd",
  givenUserAnswer: "asdf",
  expectedAnswerLetters: [
    { letter: "a", isAdditional: false, isCorrect: true } as AnswerLetter,
    { letter: "s", isAdditional: false, isCorrect: true } as AnswerLetter,
    { letter: "d", isAdditional: false, isCorrect: true } as AnswerLetter,
    { letter: "f", isAdditional: true, isCorrect: false } as AnswerLetter,
  ],
} as Context;

const AdditionalLetterInWord = {
  givenCorrectAnswer: "correct answer",
  givenUserAnswer: "ccorrect answer",
  expectedAnswerLetters: [
    { letter: "c", isAdditional: true, isCorrect: false } as AnswerLetter,
    { letter: "c", isAdditional: false, isCorrect: true } as AnswerLetter,
    { letter: "o", isAdditional: false, isCorrect: true } as AnswerLetter,
    { letter: "r", isAdditional: false, isCorrect: true } as AnswerLetter,
    { letter: "e", isAdditional: false, isCorrect: true } as AnswerLetter,
    { letter: "c", isAdditional: false, isCorrect: true } as AnswerLetter,
    { letter: "t", isAdditional: false, isCorrect: true } as AnswerLetter,
    { letter: " ", isAdditional: false, isCorrect: true } as AnswerLetter,
    { letter: "a", isAdditional: false, isCorrect: true } as AnswerLetter,
    { letter: "n", isAdditional: false, isCorrect: true } as AnswerLetter,
    { letter: "s", isAdditional: false, isCorrect: true } as AnswerLetter,
    { letter: "w", isAdditional: false, isCorrect: true } as AnswerLetter,
    { letter: "e", isAdditional: false, isCorrect: true } as AnswerLetter,
    { letter: "r", isAdditional: false, isCorrect: true } as AnswerLetter,
  ],
} as Context;

describe("getAnswerLetters", () => {
  [
    SimpleComparison,
    SentenceComparison,
    WrongLetter,
    AdditionalLetter,
    // AdditionalLetterInWord,
  ].forEach((item) => {
    it("should compare texts correctly", () => {
      const result = getAnswerLetters(item.givenCorrectAnswer, item.givenUserAnswer);

      expect(result).toStrictEqual(item.expectedAnswerLetters);
    });
  });
});
