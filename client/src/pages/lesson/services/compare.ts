export function compare(correctAnswer: string, answer: string): boolean {
  let val1 = correctAnswer.toLocaleLowerCase();
  let val2 = answer.toLocaleLowerCase();
  return val1 === val2;
}
