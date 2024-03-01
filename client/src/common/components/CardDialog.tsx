import { ReactElement } from 'react'
import Footer from './Footer'
import { CardForm, CardFormModel } from './CardForm'
import Language from 'common/models/languages'
import { Modal } from 'common/components/Modal'

export default function CardDialog({
  card,
  onHide,
  onSubmit,
  onDelete,
  frontLanguage,
  backLanguage
}: Model): ReactElement {
  const ondelete: () => void = () => {
    if (!card) return
    if (onDelete) onDelete(card)
  }

  const header = (
    <div className="bg-main rounded-t-md">
      <p className="p-5 text-xl font-extrabold">
        {card?.cardId ? 'Editing Card' : 'Creating Card'}
      </p>
    </div>
  )

  const footer = <Footer onhide={onHide} ondelete={card && card.cardId ? ondelete : undefined} />

  return (
    <Modal isOpen={card !== null} onClose={onHide} footer={footer} header={header}>
      <div className="lg:w-[50vw] w-[96vw] model">
        <CardForm
          card={card}
          onSubmit={onSubmit}
          frontLanguage={frontLanguage}
          backLanguage={backLanguage}
        />
      </div>
    </Modal>
  )
}

interface Model {
  card: CardFormModel | null
  onHide: () => void
  onSubmit: (item: CardFormModel) => void
  onDelete?: (item: CardFormModel) => void
  frontLanguage?: Language
  backLanguage?: Language
}
