import { getQuery } from '../repeats'

describe('getQuery', () => {
  ;[
    {
      request: {},
      query: '?'
    },
    {
      request: { prop1: 1 },
      query: '?prop1=1&'
    },

    {
      request: { prop1: 'asdf' },
      query: '?prop1=asdf&'
    },
    {
      request: { prop1: [1, 2] },
      query: '?prop1=1&prop1=2&'
    },
    {
      request: { prop1: ['qwer', 'asdf'] },
      query: '?prop1=qwer&prop1=asdf&'
    }
  ].forEach((item, index) => {
    it('should produce proper value ' + index, () => {
      const query = getQuery(item.request)
      expect(query).toBe(item.query)
    })
  })
})
