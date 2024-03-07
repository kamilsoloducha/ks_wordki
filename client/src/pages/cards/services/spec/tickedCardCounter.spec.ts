import { getTickedCardCount } from '../tickedCardCounter'
import * as mocks from './tickedCardCounter.mocks.spec'

describe('getTickedCardCount', () => {
  ;[
    new mocks.EmptyList(),
    new mocks.MultipeItemsDoubleSideList(),
    new mocks.SingleItemDoubleSideList(),
    new mocks.SingleItemSingleSideList()
  ].forEach((item) => {
    it('should return proper value ::' + item.constructor.name, () => {
      const result = getTickedCardCount(item.cards)
      expect(result).toBe(item.result)
    })
  })
})
