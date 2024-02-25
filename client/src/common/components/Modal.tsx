import React, { ReactNode, useRef } from 'react'

export type ModalProps = {
  isOpen: boolean
  children: ReactNode
  onClose: () => void
  header?: ReactNode
  footer?: ReactNode
}

export function Modal({
  isOpen,
  children,
  header,
  footer,
  onClose
}: ModalProps): ReactNode {
  if (!isOpen) {
    return null
  }

  const outSideClick = (event: React.MouseEvent<HTMLElement>) => {
    event.stopPropagation()
    onClose()
  }
  return (
    <section
      className="fixed inset-x-0 inset-y-0 bg-black/50 flex items-center justify-center"
      onClick={outSideClick}
    >
      <article
        onClick={(e) => {
          e.stopPropagation()
        }}
      >
        <div>{header}</div>
        <main>{children}</main>
        <div>{footer}</div>
      </article>
    </section>
  )
}
