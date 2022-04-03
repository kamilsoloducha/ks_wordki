import { CardSummary } from "../models";

export function getTickedCardCount(cards: CardSummary[]): number {
  let result = 0;
  cards.forEach((item) => {
    if (item.back.isTicked) result++;
  });
  return result;
}
