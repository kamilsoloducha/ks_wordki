import { CardSummary } from '../models'

export function getLearningCardCount(cards: CardSummary[]): number {
  let result = 0
  cards.forEach((item) => {
    if (item.front.isUsed) result++
    if (item.back.isUsed) result++
  })
  return result
}
