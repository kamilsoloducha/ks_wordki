import { buttonClick, elementClick } from 'src/__mocks__/utils.test'
import { Dropdown, TType } from 'common/components/Dropdown'
import { act, fireEvent, render, screen } from '@testing-library/react'

const items: TType[] = [
  { label: '1', value: '1' },
  { label: '2', value: '2' },
  { label: '3', value: '3' }
]

describe('Dropdown', () => {
  const onChangeFn = vi.fn()

  afterEach(() => {
    vi.clearAllMocks()
  })

  it('should open panel', async () => {
    const { container } = render(<Dropdown items={items} />)
    await elementClick(container, 'button')

    items.forEach((item) => {
      expect(screen.getByText(item.label)).toBeInTheDocument()
    })
  })

  it('should open and close panel', async () => {
    const { container } = render(<Dropdown items={items} />)
    await elementClick(container, 'button')

    expect(screen.getByTestId('drop-down-panel')).toBeInTheDocument()

    await elementClick(container, 'button')

    expect(screen.queryByTestId('drop-down-panel')).toBeFalsy()
  })

  it('should select item after click', async () => {
    const { container } = render(<Dropdown items={items} onChange={onChangeFn} />)
    await elementClick(container, 'button')

    const element = screen.getByText('1')
    await act(() => fireEvent.click(element))

    const input = container.querySelector('input') as HTMLInputElement
    expect(input.value).toBe('1')
    expect(onChangeFn).toHaveBeenCalledOnce()
    expect(onChangeFn).toHaveBeenCalledWith({ label: '1', value: '1' })
  })

  it('should select default selectedItem', async () => {
    const { container } = render(
      <Dropdown items={items} selectedItem={{ label: '1', value: '1' }} />
    )

    const input = container.querySelector('input') as HTMLInputElement
    expect(input.value).toBe('1')
  })

  it('should select default seletedIndex', async () => {
    const { container } = render(<Dropdown items={items} selectedIndex={1} />)

    const input = container.querySelector('input') as HTMLInputElement
    expect(input.value).toBe('2')
  })
})
