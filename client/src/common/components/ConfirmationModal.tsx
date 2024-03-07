import { Modal } from 'common/components/Modal'
import React from 'react'
import { ReactNode, useState } from 'react'

export const ConfirmationModal = React.forwardRef((_: any, ref: any) => {
  const [isOpen, setIsOpen] = useState<boolean>(false)
  const [message, setMessage] = useState<string>('')
  const [headerMessage, setHeaderMessage] = useState<string>('')

  const close = () => {
    setIsOpen(false)
    setMessage('')
    setHeaderMessage('')
  }

  const footer = (
    <div className="flex justify-end rounded-b-lg border-t-2 border-red-700 bg-red-800 p-2">
      <button className="rounded p-3 text-red-500 hover:bg-red-700" onClick={close}>
        Close
      </button>
    </div>
  )

  const header = (
    <div className="rounded-t-lg border-b-2 border-red-700 bg-red-800 p-5 text-2xl font-bold text-red-500">
      {headerMessage}
    </div>
  )

  const show = (message: string, header: string) => {
    setMessage(message)
    setHeaderMessage(header)
    setIsOpen(true)
  }

  React.useImperativeHandle(
    ref,
    () => {
      return {
        show
      } as ConfirmationModalRef
    },
    []
  )

  return (
    <>
      <Modal
        isOpen={isOpen}
        children={<ConfirmationModalContent message={message} />}
        footer={footer}
        header={header}
        onClose={close}
      />
    </>
  )
})

export type ConfirmationModalRef = {
  show: (message: string, header: string) => void
}

function ConfirmationModalContent({ message }: { message: string }): ReactNode {
  return <div className="w-[30rem] bg-red-800 p-5 text-red-500">{message}</div>
}
