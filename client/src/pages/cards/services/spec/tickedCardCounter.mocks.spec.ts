import { CardSummaryBuilder, SideSummaryBuilder } from '../../../../test/builders'
import { CardSummary } from '../../models'

interface TickedCardsCounter {
  cards: CardSummary[]
  result: number
}

export class EmptyList implements TickedCardsCounter {
  cards = []
  result = 0
}

export class SingleItemSingleSideList implements TickedCardsCounter {
  cards = [
    new CardSummaryBuilder()
      .withFront(new SideSummaryBuilder().withTicked(true).build())
      .withBack(new SideSummaryBuilder().withTicked(false).build())
      .build()
  ]
  result = 1
}

export class SingleItemDoubleSideList implements TickedCardsCounter {
  cards = [
    new CardSummaryBuilder()
      .withFront(new SideSummaryBuilder().withTicked(true).build())
      .withBack(new SideSummaryBuilder().withTicked(true).build())
      .build()
  ]
  result = 1
}

export class MultipeItemsDoubleSideList implements TickedCardsCounter {
  cards = [
    new CardSummaryBuilder()
      .withFront(new SideSummaryBuilder().withTicked(true).build())
      .withBack(new SideSummaryBuilder().withTicked(true).build())
      .build(),
    new CardSummaryBuilder()
      .withFront(new SideSummaryBuilder().withTicked(true).build())
      .withBack(new SideSummaryBuilder().withTicked(true).build())
      .build()
  ]
  result = 2
}
