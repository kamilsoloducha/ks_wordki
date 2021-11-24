export function compare(question: string, answer: string): boolean {
  let val1 = question.toLocaleLowerCase();
  let val2 = answer.toLocaleLowerCase();
  console.log(val1, val2);
  return val1 === val2;
}
