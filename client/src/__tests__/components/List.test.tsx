import List from '@/common/components/List'
import { render, screen } from '@testing-library/react'

describe('List', () => {
  it('should render items with props', async () => {
    const items = [
      { p1: 'test', p2: 1234 },
      { p1: 'test', p2: 1234 }
    ]
    const templateFactory = (props: { p1: string; p2: number }) => (
      <>
        <div>{props.p1}</div>
        <div>{props.p2}</div>
      </>
    )
    render(<List items={items} template={templateFactory} />)
    const elemenets = await screen.findAllByText('test')
    expect(elemenets.length).toBe(items.length)
  })
})
