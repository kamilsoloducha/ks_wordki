import { buttonClick } from 'src/__mocks__/utils.test'
import { Dropdown } from 'common/components/Dropdown'
import { act, fireEvent, render, screen } from '@testing-library/react'

const items: string[] = ['1', '2', '3']

describe('Dropdown', () => {
  const onChangeFn = vi.fn()

  afterEach(() => {
    vi.clearAllMocks()
  })

  it('should open panel', async () => {
    const { container } = render(<Dropdown items={items} />)
    await buttonClick(container, 'open')

    items.forEach((item) => {
      expect(screen.getByText(item)).toBeInTheDocument()
    })
  })

  it('should open and close panel', async () => {
    const { container } = render(<Dropdown items={items} />)
    await buttonClick(container, 'open')

    expect(screen.getByTestId('drop-down-panel')).toBeInTheDocument()

    await buttonClick(container, 'open')

    expect(screen.queryByTestId('drop-down-panel')).toBeFalsy()
  })

  it('should select item after click', async () => {
    const { container } = render(<Dropdown items={items} onChange={onChangeFn} />)
    await buttonClick(container, 'open')

    const element = screen.getByText('1')
    await act(() => fireEvent.click(element))

    const input = container.querySelector('input') as HTMLInputElement
    expect(input.value).toBe('1')
    expect(onChangeFn).toHaveBeenCalledOnce()
    expect(onChangeFn).toHaveBeenCalledWith('1')
  })

  it('should select default selectedItem', async () => {
    const { container } = render(<Dropdown items={items} selectedItem="1" />)

    const input = container.querySelector('input') as HTMLInputElement
    expect(input.value).toBe('1')
  })

  it('should select default seletedIndex', async () => {
    const { container } = render(<Dropdown items={items} selectedIndex={1} />)

    const input = container.querySelector('input') as HTMLInputElement
    expect(input.value).toBe('2')
  })
})
