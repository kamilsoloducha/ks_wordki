export function compare(correctAnswer: string, answer: string): boolean {
  let correctValues = correctAnswer
    .toLocaleLowerCase()
    .split(";")
    .map((x) => x.trim());
  let val2 = answer.toLocaleLowerCase().trim();
  return correctValues.includes(val2);
}
