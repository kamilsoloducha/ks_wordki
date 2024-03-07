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
  const onFooterDeleteClicked: () => void = () => {
    if (!card) return
    if (onDelete) onDelete(card)
  }

  const header = (
    <div className="rounded-t-md bg-main">
      <p className="p-5 text-xl font-extrabold">
        {card?.cardId ? 'Editing Card' : 'Creating Card'}
      </p>
    </div>
  )

  const footer = (
    <Footer
      onCancelClicked={onHide}
      onDeleteClicked={card && card.cardId ? onFooterDeleteClicked : undefined}
      formId="card-form"
    />
  )
  return (
    <Modal isOpen={!!card} onClose={onHide} footer={footer} header={header}>
      <div className="model w-[96vw] lg:w-[50vw]">
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
  card?: CardFormModel
  onHide: () => void
  onSubmit: (item: CardFormModel) => void
  onDelete?: (item: CardFormModel) => void
  frontLanguage?: Language
  backLanguage?: Language
}
