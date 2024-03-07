import { CardSummaryBuilder, SideSummaryBuilder } from '../../../../test/builders'
import { CardSummary } from '../../models'

interface LearningCardsCounter {
  cards: CardSummary[]
  result: number
}

export class EmptyList implements LearningCardsCounter {
  cards = []
  result = 0
}

export class SingleItemSingleSideList implements LearningCardsCounter {
  cards = [
    new CardSummaryBuilder()
      .withFront(new SideSummaryBuilder().withIsUsed(true).build())
      .withBack(new SideSummaryBuilder().withIsUsed(false).build())
      .build()
  ]
  result = 1
}

export class SingleItemDoubleSideList implements LearningCardsCounter {
  cards = [
    new CardSummaryBuilder()
      .withFront(new SideSummaryBuilder().withIsUsed(true).build())
      .withBack(new SideSummaryBuilder().withIsUsed(true).build())
      .build()
  ]
  result = 2
}

export class MultipeItemsDoubleSideList implements LearningCardsCounter {
  cards = [
    new CardSummaryBuilder()
      .withFront(new SideSummaryBuilder().withIsUsed(true).build())
      .withBack(new SideSummaryBuilder().withIsUsed(true).build())
      .build(),
    new CardSummaryBuilder()
      .withFront(new SideSummaryBuilder().withIsUsed(true).build())
      .withBack(new SideSummaryBuilder().withIsUsed(true).build())
      .build()
  ]
  result = 4
}
