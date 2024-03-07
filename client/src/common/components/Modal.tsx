import React, { ReactNode, useRef } from 'react'

export type ModalProps = {
  isOpen: boolean
  children: ReactNode
  onClose: () => void
  header?: ReactNode
  footer?: ReactNode
}

export function Modal({ isOpen, children, header, footer, onClose }: ModalProps): ReactNode {
  if (!isOpen) {
    return null
  }

  const outSideClick = (event: React.MouseEvent<HTMLElement>) => {
    event.stopPropagation()
    onClose()
  }
  return (
    <section
      className="fixed inset-0 z-50 flex items-center justify-center rounded-xl bg-black/50"
      onClick={outSideClick}
    >
      <article
        onClick={(e) => {
          e.stopPropagation()
        }}
      >
        {header}
        <main>{children}</main>
        {footer}
      </article>
    </section>
  )
}
