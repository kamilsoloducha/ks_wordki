import { Modal } from 'common/components/Modal'
import { act, fireEvent, render, screen } from '@testing-library/react'

describe('Modal', () => {
  it('should not be rendered if is not open', () => {
    const { container } = render(
      <Modal isOpen={false} onClose={() => {}}>
        Children
      </Modal>
    )

    expect(container.querySelector('section')).toBeFalsy()
  })

  it('should render all elements', async () => {
    const { container } = render(
      <Modal isOpen={true} onClose={() => {}}>
        Children
      </Modal>
    )
    expect(container.querySelector('section')).toBeInTheDocument()
    expect(await screen.findByText('Children')).toBeInTheDocument()
  })

  it('should call onClose when click outside', async () => {
    const onCloseFn = vi.fn()
    const { container } = render(
      <Modal isOpen={true} onClose={onCloseFn}>
        Children
      </Modal>
    )
    const outsideEl = container.querySelector('section') as Element
    await act(() => fireEvent.click(outsideEl))

    expect(onCloseFn).toHaveBeenCalledTimes(1)
  })

  it('should render additional Elements', async () => {
    render(
      <Modal isOpen={true} onClose={() => {}} footer={'Footer'} header={'Header'}>
        Children
      </Modal>
    )
    expect(await screen.findByText('Footer')).toBeInTheDocument()
    expect(await screen.findByText('Header')).toBeInTheDocument()
  })
})
