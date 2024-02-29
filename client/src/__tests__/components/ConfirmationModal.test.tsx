import { act, render, screen } from '@testing-library/react'
import { ConfirmationModal, ConfirmationModalRef } from 'common/components/ConfirmationModal'
import React from 'react'

describe('ConfirmationModal', () => {
  let confirmationModalRef: any

  beforeEach(() => {
    confirmationModalRef = React.createRef<ConfirmationModalRef>()
  })

  it('should be open after call', async () => {
    render(<ConfirmationModal ref={confirmationModalRef} />)
    await act(() => confirmationModalRef.current.show('MessageTest', 'HeaderTest'))

    expect(screen.queryByText('MessageTest')).toBeInTheDocument()
    expect(screen.queryByText('HeaderTest')).toBeInTheDocument()
  })
})
