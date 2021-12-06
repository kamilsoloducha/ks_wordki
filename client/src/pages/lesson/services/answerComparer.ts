import { AnswerLetter } from "../models/answer";

export function compare(correctAnswer: string, answer: string): boolean {
  let val1 = correctAnswer.toLocaleLowerCase();
  let val2 = answer.toLocaleLowerCase();
  return val1 === val2;
}

export function getAnswerLetters(
  correctAnswer: string,
  answer: string
): AnswerLetter[] {
  const result: AnswerLetter[] = [];
  let i = 0;
  for (; i < correctAnswer.length; i++) {
    const correctLetter = correctAnswer[i];
    const answerLetter = i < answer.length ? answer[i] : "";

    result.push({
      letter: correctLetter,
      isCorrect: correctLetter === answerLetter,
      isAdditional: false,
    } as AnswerLetter);
  }

  if (correctAnswer.length < answer.length) {
    for (; i < answer.length; i++) {
      const additionalLetter = answer[i];

      result.push({
        letter: additionalLetter,
        isCorrect: false,
        isAdditional: true,
      } as AnswerLetter);
    }
  }

  return result;
}
