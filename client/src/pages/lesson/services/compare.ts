export function compare(correctAnswer: string, answer: string): boolean {
  const correctValues = correctAnswer
    .toLocaleLowerCase()
    .split(";")
    .map((x) => x.replaceAll("sth", "").replaceAll("sb", "").trim());
  return correctValues.includes(
    answer.toLocaleLowerCase().replaceAll("sth", "").replaceAll("sb", "").trim()
  );
}
