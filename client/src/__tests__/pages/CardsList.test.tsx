import CardsList from 'pages/cards/components/CardsList'
import { CardSummary } from 'pages/cards/models'
import { fireEvent, render } from '@testing-library/react'

describe('CardsList', () => {
  const mockCards: CardSummary[] = [
    {
      id: '1',
      front: {
        type: 0,
        value: '',
        example: '',
        comment: '',
        drawer: 0,
        isUsed: false,
        isTicked: false
      },
      back: {
        type: 0,
        value: '',
        example: '',
        comment: '',
        drawer: 0,
        isUsed: false,
        isTicked: false
      }
    },
    {
      id: '2',
      front: {
        type: 0,
        value: '',
        example: '',
        comment: '',
        drawer: 0,
        isUsed: false,
        isTicked: false
      },
      back: {
        type: 0,
        value: '',
        example: '',
        comment: '',
        drawer: 0,
        isUsed: false,
        isTicked: false
      }
    }
  ]

  it('renders without items', () => {
    render(<CardsList cards={[]} />)
  })

  it('renders correct number of cards', () => {
    const { container } = render(<CardsList cards={mockCards} />)
    expect(container.querySelectorAll('.cursor-pointer').length).toBe(mockCards.length)
  })

  it('calls onItemSelected when a card is clicked', () => {
    const mockOnItemSelected = vi.fn()
    const { container } = render(<CardsList cards={mockCards} onItemSelected={mockOnItemSelected} />)

    const cardItems = container.querySelectorAll('.cursor-pointer')
    fireEvent.click(cardItems[0])

    expect(mockOnItemSelected).toHaveBeenCalledWith(mockCards[0])
  })

  it('does not call onItemSelected if not provided', () => {
    const { container } = render(<CardsList cards={mockCards} />)

    const cardItems = container.querySelectorAll('.cursor-pointer')
    fireEvent.click(cardItems[0])
  })
})
