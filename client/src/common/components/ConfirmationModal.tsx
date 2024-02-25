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
    <div className="bg-red-800 flex justify-end rounded-b-lg p-2 border-t-2 border-red-700">
      <button className="hover:bg-red-700 text-red-500 rounded p-3" onClick={close}>
        Close
      </button>
    </div>
  )

  const header = (
    <div className="bg-red-800 text-2xl text-red-500 font-bold rounded-t-lg p-5 border-b-2 border-red-700">
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
  return <div className="bg-red-800 text-red-500 p-5 w-[30rem]">{message}</div>
}
