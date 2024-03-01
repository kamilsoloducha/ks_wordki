import { Language } from 'pages/lessonSettings/models/languages'
import { ReactElement } from 'react'
import Footer from './Footer'
import { GroupForm, GroupFormModel } from './GroupForm'
import { Modal } from 'common/components/Modal'

export default function GroupDialog({
  cardSides,
  group,
  onHide,
  onSubmit,
  onDelete
}: GroupDialogProps): ReactElement {
  const ondelete: () => void = () => {
    if (onDelete && group?.id) onDelete(group.id)
  }

  const footer = <Footer onhide={onHide} ondelete={ondelete} />

  const header = (
    <div className="bg-main rounded-t-md">
      <p className="p-5 text-xl font-extrabold">{group?.id ? 'Editing Group' : 'Creating Group'}</p>
    </div>
  )

  return (
    <Modal isOpen={group !== null} onClose={onHide} footer={footer} header={header}>
      <div className="lg:w-[50vw] w-[96vw] model">
        <GroupForm options={cardSides.map((x) => x.language)} group={group} onSubmit={onSubmit} />
      </div>
    </Modal>
  )
}

type GroupDialogProps = {
  group: GroupFormModel
  cardSides: Language[]
  onHide: () => void
  onSubmit: (item: GroupFormModel) => void
  onDelete?: (groupId: string) => void
}
