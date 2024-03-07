import { CardSummary, SideSummary } from 'pages/cards/models'
import { SideSummaryBuilder } from './sideSummary.mocks.spec'

export class CardSummaryBuilder implements CardSummary {
  id = 'id'
  front = new SideSummaryBuilder().build()
  back = new SideSummaryBuilder().build()

  withFront(value: SideSummary): CardSummaryBuilder {
    this.front = value
    return this
  }

  withBack(value: SideSummary): CardSummaryBuilder {
    this.back = value
    return this
  }

  build(): CardSummary {
    return this as CardSummary
  }
}
