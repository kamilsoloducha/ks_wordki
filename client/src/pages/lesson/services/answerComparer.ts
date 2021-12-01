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

  for (let i = 0; i < correctAnswer.length; i++) {
    const correctLetter = correctAnswer[i];
    const answerLetter = i < answer.length ? answer[i] : "";

    result.push({
      letter: correctLetter,
      isCorrect: correctLetter === answerLetter,
    } as AnswerLetter);
  }

  return result;
}
