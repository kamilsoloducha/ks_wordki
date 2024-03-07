import { directElementClick } from 'src/__mocks__/utils.test'
import { render, screen } from '@testing-library/react'
import { TabView, TabViewItemModel } from 'common/components/TabView'

describe('TabView', () => {
  const onItemChangedFn = vi.fn()
  const items: TabViewItemModel[] = [
    { value: 1, header: 'Header1', element: <>Element1</> },
    { value: 2, header: 'Header2', element: <>Element2</> }
  ]

  afterEach(() => {
    vi.resetAllMocks()
  })

  it('should render items', () => {
    render(<TabView items={items} selectedValue={1} onItemChanged={onItemChangedFn} />)

    expect(screen.getByText('Header1')).toBeInTheDocument()
    expect(screen.getByText('Header2')).toBeInTheDocument()
    expect(screen.getByText('Element1')).toBeInTheDocument()
  })

  it('should render container if items is empty', () => {
    const { container } = render(
      <TabView items={[]} selectedValue={1} onItemChanged={onItemChangedFn} />
    )

    expect(container.querySelectorAll('div').length).toBe(2)
  })

  it('should call method after click', async () => {
    render(<TabView items={items} selectedValue={1} onItemChanged={onItemChangedFn} />)
    await directElementClick(screen.getByText('Header2'))

    expect(onItemChangedFn).toHaveBeenCalledOnce()
    expect(onItemChangedFn).toHaveBeenCalledWith(2)

    await directElementClick(screen.getByText('Header1'))

    expect(onItemChangedFn).toHaveBeenCalledWith(1)
  })
})
