import { TabViewItemHeader } from 'common/components/TabViewItemHeader'
import { render, screen } from '@testing-library/react'
import { elementClick } from 'src/__mocks__/utils.test'

describe('TabViewItemHeader', () => {
  const onClickFn = vi.fn()
  const header = 'Test Header'
  const value = 1

  let container: HTMLElement

  afterEach(() => {
    vi.clearAllMocks()
  })
  ;[{ isSelected: true }, { isSelected: false }].forEach((item, index) => {
    describe('when it has render :: ' + index, () => {
      beforeEach(() => {
        container = render(
          <TabViewItemHeader
            header={header}
            value={value}
            isSelected={item.isSelected}
            onClick={onClickFn}
          />
        ).container
      })

      it('should render header ' + index, () => {
        expect(screen.getByText(header, { exact: false })).toBeInTheDocument()
      })

      it('should call method after click', async () => {
        await elementClick(container, 'div')

        expect(onClickFn).toHaveBeenCalledOnce()
        expect(onClickFn).toHaveBeenCalledWith(1)
      })
    })
  })
})
