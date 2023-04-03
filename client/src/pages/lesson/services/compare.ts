export function compare(correctAnswer: string, answer: string): boolean {
  const correctValues = correctAnswer
    .toLocaleLowerCase()
    .split(";")
    .map((x) => x.trim());
  const val2 = answer.toLocaleLowerCase().trim();
  return correctValues.includes(val2);
}
